//Copyright(C) 2026 Estlib

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <https://gnu.org>.

using FFMpegCore;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTA.Controllers;
using YTA.DBDomain.Models;

namespace YTA
{
    public partial class Form1 : Form
    {
        private readonly ContentController _contentController = new ContentController();
        private readonly YoutubeAPIController _youtubeAPIController = new YoutubeAPIController();
        private readonly PrefabController _prefabController = new PrefabController();

        private DateTime _lastScanTime = DateTime.Now;
        private DateTime _nextScanTime = DateTime.Now;
        private bool _tickerBusy = false;
        private bool _loadingPrefabCategories = false;
        private bool _loadingPrefabs = false;
        private Guid? _currentlyEditingPrefabID = null;
        private bool _isOnTray = false;
        private bool _OSMSGEnable = true;
        private bool _reminders = true;

        public Form1()
        {
            InitializeComponent();
            SetupComboEnum();
            SetupTicker();
            LoadPrefabs();
            Settingsetter();
        }

        private void Settingsetter()
        {
            tickBubbles.Checked = _OSMSGEnable;
            tickReminders.Checked = _reminders;
        }

        /// <summary>
        /// Loads user-created prefab settings from db
        /// </summary>
        private void LoadPrefabs()
        {
            _loadingPrefabs = true;
            List<Prefab> prefabsNone = new List<Prefab>();

            prefabsNone.Add(new Prefab
            {
                ID = Guid.Empty,
                PrefabName = "None"
            });

            var result = _prefabController.GetAllPrefabs();

            if (result != null && result.Count > 0)
            {
                prefabsNone.AddRange(result);
            }

            cboxWhichPrefab.DataSource = null;
            cboxWhichPrefab.DisplayMember = "PrefabName";
            cboxWhichPrefab.ValueMember = "ID";
            cboxWhichPrefab.DataSource = prefabsNone.ToList();
            cboxWhichPrefab.SelectedIndex = 0;

            cboxWhichPrefab_PF.DataSource = null;
            cboxWhichPrefab_PF.DisplayMember = "PrefabName";
            cboxWhichPrefab_PF.ValueMember = "ID";
            cboxWhichPrefab_PF.DataSource = prefabsNone.ToList();
            cboxWhichPrefab_PF.SelectedIndex = 0;

            _loadingPrefabs = false;
        }
        /// <summary>
        /// Configures the upload ticker with the specified interval and updates its state.
        /// </summary>
        /// <remarks>This method sets the ticker interval based on the configured value, initializes the
        /// last and next scan times,  and updates the ticker's enabled state and associated UI elements. The ticker is
        /// initially disabled after setup.</remarks>
        private void SetupTicker()
        {
            uploadTicker.Interval = (int)tickerIntervalSetting.Value * 60 * 1000;
            uploadTicker.Enabled = false;
            _lastScanTime = DateTime.Now;
            _nextScanTime = DateTime.Now.AddMilliseconds(uploadTicker.Interval);
            tickerOnOff.Checked = uploadTicker.Enabled;
            if (tickerOnOff.Checked)
            {
                tickerOnOff.Text = "Is running";
            }
            else
            {
                tickerOnOff.Text = "Stopped";
            }
            UpdateScanInfo();
        }
        /// <summary>
        /// Updates the displayed scan information, including the last scan time, next scan time,  and the number of
        /// uploaded items.
        /// </summary>
        /// <remarks>This method updates the text boxes with the latest scan-related data. The values are 
        /// retrieved from internal fields and the content controller.</remarks>
        private void UpdateScanInfo()
        {
            tboxWhenLastScan.Text = _lastScanTime.ToString();
            tboxNextScanTime.Text = _nextScanTime.ToString();
            tboxHowManyUploaded.Text = _contentController.CountUploaded().ToString();
        }

        // form element methods

        /// <summary>
        /// Populates the data sources of combo box controls with the values of the corresponding enumeration types.
        /// </summary>
        /// <remarks>This method sets the data sources of the combo boxes to the values of the <see
        /// cref="MediaType"/> and <see cref="PrivacyType"/> enumerations. This allows the combo boxes to display all
        /// possible values of these enumerations for user selection.</remarks>
        private void SetupComboEnum()
        {
            cboxMediaType.DataSource = Enum.GetValues(typeof(MediaType));
            cboxMediaType_PF.DataSource = Enum.GetValues(typeof(MediaType));
            cboxPrivacy.DataSource = Enum.GetValues(typeof(PrivacyType));
            cboxPrivacyType_PF.DataSource = Enum.GetValues(typeof(PrivacyType));
        }





        /// <summary>
        /// Refreshes Data seen in database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshData_Click(object sender, EventArgs e)
        {
            GetDataFromDB();
        }
        /// <summary>
        /// Handles the click event of the "Add Entry" button, creating a new YouTube content entry and saving it to the
        /// database if the input data is valid.
        /// </summary>
        /// <remarks>This method validates the input data, constructs a new <see cref="YTContent"/> object
        /// with the provided details, and saves it using the content controller. If the input data is invalid, the
        /// method exits without performing any action.</remarks>
        /// <param name="sender">The source of the event, typically the "Add Entry" button.</param>
        /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            List<YTPlaylist> listsVideoBelongsTo = new List<YTPlaylist>();
            listsVideoBelongsTo = FindSelectedLists(fboxPlaylists);


            bool result = ValidateModel();
            if (result)
            {
                YTContent newEntry = new YTContent()
                {
                    ThisMediaIs = (MediaType)cboxMediaType.SelectedItem,
                    LocalVideoPath = tboxVideopath.Text,
                    LocalImagePath = tboxImagepath.Text,

                    Title = tboxTitle.Text,
                    Description = tboxDescription.Text,
                    VideoTags = tboxTags.Text,

                    CategoryID = cboxCategory.SelectedValue.ToString(),
                    Privacy = (PrivacyType)cboxPrivacy.SelectedItem,

                    SelfDeclaredMadeForKids = tickChild.Checked,
                    ContainsSyntheticMedia = tickRobot.Checked,
                    HasPaidProductPlacement = tickProduct.Checked,

                    YTAHandleTime_CreatedAt = DateTime.Now,
                    YTAHandleTime_ModifiedAt = DateTime.Now,
                    YTAHandleTime_PassedToYTAt = dateTimePicker1.Value,

                    ListsIds = string.Join(",", listsVideoBelongsTo.Select(x => x.ListID)),
                    ListsNames = string.Join(", ", listsVideoBelongsTo.Select(x => x.ListName)),
                };
                _contentController.Create(newEntry);
                string title = "Infos";
                string message = "Attempting save.";
                var result2 = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetDataFromDB();
            }
            else
            {
                return;
            }
        }

        private List<YTPlaylist> FindSelectedLists(FlowLayoutPanel uiElement)
        {
            List<YTPlaylist> listsVideoBelongsTo = new List<YTPlaylist>();
            foreach (Control cardlist in uiElement.Controls)
            {
                foreach (Control cardElement in cardlist.Controls)
                {
                    if (cardElement is CheckBox tick && tick.Checked && tick.Tag is YTPlaylist playlist)
                    {
                        listsVideoBelongsTo.Add(playlist);
                    }
                }
            }
            return listsVideoBelongsTo;
        }

        /// <summary>
        /// find da file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImageBrowser_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialog = new OpenFileDialog();

            dialog.Title = "Select image/thumbnail file";
            dialog.Filter = "Image files|*.jpg;*.jpeg;*.png;*.webp|All files|*.*";
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tboxImagepath.Text = dialog.FileName;
            }
        }

        /// <summary>
        /// find da image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileBrowser_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialog = new OpenFileDialog();

            dialog.Title = "Select video file";
            dialog.Filter = "Video files|*.mp4;*.mov;*.avi;*.mkv;*.webm|All files|*.*";
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                double duration = DurationCheck(dialog.FileName);
                if (duration < 61 && (MediaType)cboxMediaType.SelectedItem != MediaType.Short)
                {
                    string title = "Infos";
                    string message = $"Setting entry type to Short, as video is too short for a normal upload.";
                    var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboxMediaType.SelectedItem = MediaType.Short;
                }
                if (duration > 60 && (MediaType)cboxMediaType.SelectedItem != MediaType.Video)
                {
                    string title = "Infos";
                    string message = $"Setting entry type to Video, as video is too long for a short.";
                    var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboxMediaType.SelectedItem = MediaType.Video;
                }


                tboxVideopath.Text = dialog.FileName;
            }
        }

        private double DurationCheck(string fileName)
        {
            IMediaAnalysis mediaInfo = FFProbe.Analyse(fileName);
            return (double)mediaInfo.Duration.TotalSeconds;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                btnLogin.Enabled = false;
                labelIsLoggedIn.Text = "Wait..";
                YouTubeService ytServ = await _youtubeAPIController.GetYouTubeServiceAsync();
                string title = "Infos";
                string message = "Login Succeeded";
                var result2 = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                YTLoggedUser channelInfo = await _youtubeAPIController.GetChannelDetails();
                channelTitleBig.Text = channelInfo.ChannelName;
                labelChannelName.Text = channelInfo.ChannelID;
                subCount.Text = channelInfo.SubCount.ToString();
                videoCount.Text = channelInfo.VideoCount.ToString();
                channelViewCount.Text = channelInfo.ChannelViewCount.ToString();
                LoadChannelThumbnail(channelInfo.AvatarLink);
                labelIsLoggedIn.Text = "Login success";
                await GetYTCategories();
                await GetYTCategoriesPrefab();
                await GetUserLists();
            }
            catch (Exception ex)
            {

                string title = "Error";
                string message = $"Login failed, see exception:\n\n+{ex.ToString()}";
                var result2 = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnLogin.Enabled = true;
        }

        private void LoadChannelThumbnail(string avatarLink)
        {
            if (string.IsNullOrWhiteSpace(avatarLink))
            {
                avatarBigImage.Image = null;
                return;
            }

            try
            {
                using HttpClient client = new HttpClient();

                byte[] imageBytes = client.GetByteArrayAsync(avatarLink).Result;

                using MemoryStream memoryStream = new MemoryStream(imageBytes);

                avatarBigImage.Image?.Dispose();
                avatarBigImage.Image = Image.FromStream(memoryStream);
            }
            catch
            {
                avatarBigImage.Image = null;
            }
        }

        private void scheduleCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            RefreshCalendarData();
        }
        private async void btnUploadNow_Click(object sender, EventArgs e)
        {
            YTContent? selected = dataEntryDisplay.CurrentRow?.DataBoundItem as YTContent;
            if (selected == null)
            {
                string title = "Infos";
                string message = $"Pick a thing, cant do thing without selecting a thing.";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // missing validation
            try
            {
                btnUploadNow.Enabled = false;
                btnUploadNow.Text = "Wait..";

                string videoID = await _youtubeAPIController.CreateContent(selected);
                selected.VideoID = videoID;
                selected.VideoLink = "https://www.youtube.com/watch?v=" + videoID;
                selected.YTAHandleTime_ModifiedAt = DateTime.Now;
                selected.YTAHandleTime_PassedToYTAt = DateTime.Now;
                _contentController.Update(selected);
                if (string.IsNullOrWhiteSpace(selected.LocalImagePath) == false)
                {
                    await _youtubeAPIController.ThumbnailUpload(videoID, selected.LocalImagePath);
                }
                string title = "Good";
                string message = $"Upload completed, link:\n\n{selected.VideoLink}";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetDataFromDB();

            }
            catch (Exception ex)
            {
                string title = "Error";
                string message = $"Upload failed, see why:\n\n{ex.Message}";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                btnUploadNow.Text = "Upload immediately";
                btnUploadNow.Enabled = true;
            }
        }

        // my shit

        private void RefreshCalendarData()
        {
            DateTime selectedDay = scheduleCalendar.SelectionStart.Date;
            List<YTContent> onThisDay = _contentController.GetRange(selectedDay, selectedDay.AddDays(1));
            scheduledEntries.Controls.Clear();
            foreach (YTContent content in onThisDay)
            {
                Control card = NewCard(content);
                scheduledEntries.Controls.Add(card);
            }
        }

        private Control NewCard(YTContent content)
        {
            Panel card = new Panel();

            card.Width = 200;
            card.Height = 180;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Margin = new Padding(8);
            card.Tag = content;

            PictureBox thumbnail = new PictureBox();

            thumbnail.Width = 180;
            thumbnail.Height = 100;
            thumbnail.Left = 10;
            thumbnail.Top = 10;
            thumbnail.SizeMode = PictureBoxSizeMode.Zoom;
            thumbnail.BorderStyle = BorderStyle.FixedSingle;

            if (!string.IsNullOrWhiteSpace(content.LocalImagePath) && File.Exists(content.LocalImagePath))
            {
                thumbnail.Image = LoadImageWithoutLocking(content.LocalImagePath);
            }

            Label title = new Label();

            title.Text = content.Title;
            title.Left = 10;
            title.Top = 120;
            title.Width = 180;
            title.Height = 35;
            title.AutoEllipsis = true;

            Label time = new Label();

            time.Text = content.YTAHandleTime_PassedToYTAt.ToString("HH:mm");
            time.Left = 10;
            time.Top = 155;
            time.Width = 180;
            time.Height = 20;

            card.Controls.Add(thumbnail);
            card.Controls.Add(title);
            card.Controls.Add(time);

            card.Click += CalendarEntryCard_Click;
            thumbnail.Click += CalendarEntryCard_Click;
            title.Click += CalendarEntryCard_Click;
            time.Click += CalendarEntryCard_Click;

            return card;
        }
        private Image LoadImageWithoutLocking(string path)
        {
            using FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return Image.FromStream(stream);
        }
        private void CalendarEntryCard_Click(object? sender, EventArgs e)
        {
            //clanker made this
            Control? clickedControl = sender as Control;

            if (clickedControl == null)
                return;

            Panel? card = clickedControl as Panel;

            if (card == null)
                card = clickedControl.Parent as Panel;

            if (card == null)
                return;

            YTContent? selectedEntry = card.Tag as YTContent;

            if (selectedEntry == null)
                return;

            MessageBox.Show(selectedEntry.Title);
        }

        private void GetDataFromDB()
        {
            List<YTContent> entries = _contentController.GetAll();
            dataEntryDisplay.DataSource = null;
            dataEntryDisplay.DataSource = entries;
            string message = "";
            string title = "Infos";
            if (entries == null)
            {
                message = "Nothing was returned, is null";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (entries.Count < 1)
            {
                message = "No entries found.";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        /// <summary>
        /// checks for required data in view
        /// </summary>
        /// <returns>true or false if anythign is missing, and a report of what is missing</returns>
        private bool ValidateModel()
        {
            string missingData = string.Empty;
            List<bool> dataPresence = new List<bool>();
            if (cboxMediaType.SelectedItem == null)
            {
                missingData += "○ Media type is not selected\n";
                dataPresence.Add(false);
            }
            else
            {
                dataPresence.Add(true);
            }
            if (cboxPrivacy.SelectedItem == null)
            {
                missingData += "○ Privacy is not selected\n";
                dataPresence.Add(false);
            }
            else
            {
                dataPresence.Add(true);
            }
            if ((MediaType)cboxMediaType.SelectedItem != MediaType.Post)
            {
                if (string.IsNullOrWhiteSpace(tboxVideopath.Text))
                {
                    missingData += "○ Video file not selected\n";
                    dataPresence.Add(false);
                }
                else if (!File.Exists(tboxVideopath.Text))
                {
                    missingData += "○ Video file does not exist\n";
                    dataPresence.Add(false);
                }
                else
                {
                    dataPresence.Add(true);
                }
            }
            if (string.IsNullOrWhiteSpace(tboxImagepath.Text))
            {
                missingData += "○ Accompanying thumbnail not selected\n";
                dataPresence.Add(false);
            }
            else
            {
                dataPresence.Add(true);
            }
            if (!File.Exists(tboxImagepath.Text))
            {
                missingData += "○ Selected accompanying thumbnail does not exist at defined path\n";
                dataPresence.Add(false);
            }
            else
            {
                dataPresence.Add(true);
            }
            if (string.IsNullOrWhiteSpace(tboxTitle.Text))
            {
                missingData += "○ Title is missing\n";
                dataPresence.Add(false);
            }
            else
            {
                dataPresence.Add(true);
            }
            if (string.IsNullOrWhiteSpace(tboxDescription.Text))
            {
                missingData += "○ Description is missing.\n";
                dataPresence.Add(false);
            }
            else if (tboxDescription.Text.Count() > 5000)
            {
                missingData += "○ Description exceeds 5000 characters.\n";
                dataPresence.Add(false);
            }
            else
            {
                dataPresence.Add(true);
            }
            if (string.IsNullOrWhiteSpace(tboxTags.Text))
            {
                missingData += "○ No tags entered. (500 char limit)\n";
                dataPresence.Add(false);
            }
            else if (tboxTags.Text.Length > 500)
            {
                missingData += "○ Too many characters used for tags. (500 max)\n";
                dataPresence.Add(false);
            }
            else
            {
                dataPresence.Add(true);
            }
            var resultCat = cboxCategory.SelectedItem;
            if (resultCat == null)
            {
                missingData += "○ Category not selected (Login needed to see categories)\n";
                dataPresence.Add(false);
            }
            else
            {
                dataPresence.Add(true);
            }


            if (dataPresence.Contains(false))
            {
                string message = $"Some required data has not been filled, see below:\n\n{missingData}";
                string title = "Error";
                if (_isOnTray == true && _OSMSGEnable == true)
                {
                    trayIcon.ShowBalloonTip(3000, title, message, ToolTipIcon.Error
                        );
                }
                else
                {
                    var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnRefreshCalendar_Click(object sender, EventArgs e)
        {
            RefreshCalendarData();
        }

        private async void btnLogOut_Click(object sender, EventArgs e)
        {
            await _youtubeAPIController.DiscardYoutubeOAuthToken();
            labelIsLoggedIn.Text = "IsLoggedOut";
            channelTitleBig.Text = "none";
            labelChannelName.Text = "Channel Name";
            subCount.Text = "0";
            videoCount.Text = "0";
            channelViewCount.Text = "0";
            avatarBigImage.Image?.Dispose();
            avatarBigImage.Image = null;
            string title = "Warnings";
            string message = "You are logged out.\n\n" +
                "Please note that the ticker will not stop, as the local entries are still scheduled.\n\n Next" +
                " time you log in (even with a different user), anything and everything up to that point in time will be uploaded to whoever logs in.";
            var result2 = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            btnLogin.Enabled = true;
            btnLogOut.Enabled = false;
        }
        private void tickerOnOff_CheckedChanged(object sender, EventArgs e)
        {

            if (tickerOnOff.Checked)
            {
                _lastScanTime = DateTime.Now;
                _nextScanTime = DateTime.Now.AddMilliseconds(uploadTicker.Interval);
                uploadTicker.Start();
                tickerOnOff.Text = "Is Running";
                UpdateScanInfo();
            }
            else
            {
                uploadTicker.Stop();
                tickerOnOff.Text = "Stopped";
            }
        }

        private void tickerIntervalSetting_ValueChanged_1(object sender, EventArgs e)
        {
            uploadTicker.Interval = (int)tickerIntervalSetting.Value * 60 * 1000;
            _nextScanTime = DateTime.Now.AddMilliseconds(uploadTicker.Interval);
            UpdateScanInfo();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (tickTrayMinimizeSetting.Checked == true && WindowState == FormWindowState.Minimized)
            {
                Hide();
                trayIcon.Visible = true;
                trayIcon.ShowBalloonTip(2000, "YTA", "YTA is working in tray.\nTo open window again, double click tray icon.", ToolTipIcon.Info);
            }
        }

        private void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            BringToFront();
            Activate();
            trayIcon.Visible = false;
        }

        private void tickTrayMinimizeSetting_CheckedChanged(object sender, EventArgs e)
        {
            if (tickTrayMinimizeSetting.Checked)
            {
                tickTrayMinimizeSetting.Text = "Will go to tray";
            }
            else
            {
                tickTrayMinimizeSetting.Text = "Minimizes to taskbar";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            trayIcon.Visible = false;
        }

        private async void uploadTicker_Tick(object sender, EventArgs e)
        {
            await UploadCycle();
        }

        private async Task UploadCycle()
        {
            if (_tickerBusy)
            {
                // log skipped bcus busy
                LogTool("Cycle skipped because previous cycle is not finished");
                return;
            }
            try
            {
                _tickerBusy = true;
                DateTime thisScanTime = DateTime.Now;
                LogTool("Cycle started.");

                List<YTContent> toUpload = _contentController.GetUploadQueue(_lastScanTime, thisScanTime); if (_isOnTray == true && _OSMSGEnable == true)
                {
                    trayIcon.ShowBalloonTip(3000, "Upload procedure", $"Uploading {toUpload.Count} entries", ToolTipIcon.Info
                        );
                }
                LogTool($"Found {toUpload.Count} in queue");
                foreach (var queueEntry in toUpload)
                {
                    LogTool($"Uploading \"{queueEntry.Title}\"");
                    if (_isOnTray == true && _OSMSGEnable == true)
                    {
                        trayIcon.ShowBalloonTip(3000, "Upload procedure", $"Uploading {queueEntry.Title}", ToolTipIcon.Info
                            );
                    }
                    string videoID = await _youtubeAPIController.CreateContent(queueEntry);
                    queueEntry.VideoID = videoID;
                    queueEntry.VideoLink = "https://www.youtube.com/watch?v=" + videoID;
                    queueEntry.YTAHandleTime_ModifiedAt = DateTime.Now;
                    queueEntry.YTAHandleTime_PassedToYTAt = DateTime.Now;
                    _contentController.Update(queueEntry);
                    if (!string.IsNullOrWhiteSpace(queueEntry.LocalImagePath))
                    {
                        await _youtubeAPIController.ThumbnailUpload(videoID, queueEntry.LocalImagePath);
                        //log thumb
                    }
                    LogTool($"\"{queueEntry.Title}\" uploaded.");
                    if (_reminders == true)
                    {
                        if (queueEntry.ThisMediaIs == MediaType.Video)
                        {
                            if (queueEntry.CategoryID == "20")
                            {
                                string title = "REMINDER";
                                string message = $"Add game name and end screens on youtubes own editor";
                                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                string title = "REMINDER";
                                string message = $"Add end screens on youtubes own editor";
                                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        else if (queueEntry.ThisMediaIs == MediaType.Short)
                        {

                            if (queueEntry.CategoryID == "20")
                            {
                                string title = "REMINDER";
                                string message = $"Add game name and related video on youtubes own editor\n{queueEntry.VideoLink}";
                                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                string title = "REMINDER";
                                string message = $"Add related video on youtubes own editor\n{queueEntry.VideoLink}";
                                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                }
                _lastScanTime = thisScanTime;
                _nextScanTime = DateTime.Now.AddMilliseconds(uploadTicker.Interval);
                LogTool("Cycle finished.");
                if (_isOnTray == true && _OSMSGEnable == true)
                {
                    trayIcon.ShowBalloonTip(3000, "Upload procedure", $"Uploading finished", ToolTipIcon.Info
                        );
                }

            }
            catch (Exception ex)
            {
                LogTool($"Error, see details:\n\n{ex.Message}");
                if (_isOnTray == true && _OSMSGEnable == true)
                {
                    trayIcon.ShowBalloonTip(3000, "Upload procedure", $"Uploading {ex.Message}", ToolTipIcon.Error
                        );
                }
            }
            finally
            {
                _tickerBusy = false;

                UpdateScanInfo();
                GetDataFromDB();
            }
        }
        /// <summary>
        /// Logs a message to the ticker log with a timestamp.
        /// </summary>
        /// <remarks>The log entry includes the current date and time in the format "dd MMMM | HH:mm:ss",
        /// followed by the provided message. Each log entry is appended to the existing content.</remarks>
        /// <param name="v">The message to log. Cannot be null or empty.</param>
        private void LogTool(string v)
        {
            tboxTickerLog.AppendText($"[{DateTime.Now.ToString("dd MMMM | HH:mm:ss")}] - " + v + Environment.NewLine);

        }
        /// <summary>
        /// Handles the click event of the Delete Row button, allowing the user to delete the currently selected row.
        /// </summary>
        /// <remarks>If no row is selected, a message box is displayed to inform the user that a selection
        /// is required. When a row is selected, the corresponding data is deleted, and the data grid is refreshed to
        /// reflect the changes.</remarks>
        /// <param name="sender">The source of the event, typically the Delete Row button.</param>
        /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            YTContent? selected = dataEntryDisplay.CurrentRow?.DataBoundItem as YTContent;
            if (selected == null)
            {
                string title = "Infos";
                string message = $"Pick a thing, cant do thing without selecting a thing.";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _contentController.Delete(selected);
            GetDataFromDB();
        }
        /// <summary>
        /// Handles the <see cref="TabControl.SelectedIndexChanged"/> event for <c>tabControl1</c>. Updates the
        /// application state based on the selected tab.
        /// </summary>
        /// <remarks>Performs different actions depending on the selected tab: <list type="bullet"> <item>
        /// <description>If the selected tab is <c>calendarTab</c>, the calendar data is refreshed.</description>
        /// </item> <item> <description>If the selected tab is <c>entriesTab</c>, data is retrieved from the database,
        /// and YouTube categories and user lists are loaded asynchronously.</description> </item> <item>
        /// <description>If the selected tab is <c>newPrefab</c>, YouTube categories for prefabs are loaded
        /// asynchronously, and prefab fields are cleared.</description> </item> </list></remarks>
        /// <param name="sender">The source of the event, typically <c>tabControl1</c>.</param>
        /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == calendarTab)
            {
                RefreshCalendarData();
            }
            if (tabControl1.SelectedTab == entriesTab)
            {
                GetDataFromDB();
                await GetYTCategories();
                await GetUserLists();
            }
            if (tabControl1.SelectedTab == newPrefab)
            {
                await GetYTCategoriesPrefab();
                ClearPrefabFields();
            }
        }
        /// <summary>
        /// Retrieves the user's YouTube playlists asynchronously and updates the associated UI controls.
        /// </summary>
        /// <remarks>This method fetches a list of YouTube playlists using the YouTube API controller and
        /// populates the corresponding UI elements with playlist cards. It clears the existing controls in the target
        /// containers before adding the new playlist cards. If an error occurs during the operation, an error message
        /// is displayed to the user.</remarks>
        /// <returns></returns>
        private async Task GetUserLists()
        {
            try
            {
                List<YTPlaylist> myLists = await _youtubeAPIController.GetLists();
                fboxPlaylists.Controls.Clear();
                fboxLists_PF.Controls.Clear();

                foreach (var list in myLists)
                {
                    Control card = ListCard(list);
                    Control card2 = ListCard(list);
                    fboxPlaylists.Controls.Add(card);
                    fboxLists_PF.Controls.Add(card2);
                }
            }
            catch (Exception ex)
            {
                string title = "Error";
                string message = $"idk wat de fuk wrong, lookie:\n\n{ex.Message}";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Control ListCard(YTPlaylist playlist)
        {
            //this is all clanker code

            Panel card = new Panel();
            card.Width = 140;
            card.Height = 100;
            card.Margin = new Padding(5);
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Tag = playlist;

            // tickbox, top-left
            CheckBox tick = new CheckBox();
            tick.Left = 3;
            tick.Top = 3;
            tick.Width = 15;
            tick.Height = 15;
            tick.Tag = playlist;

            // red area: playlist name
            Label name = new Label();
            name.Left = 20;
            name.Top = 2;
            name.Width = 115;
            name.Height = 18;
            name.Text = playlist.ListName;
            name.AutoEllipsis = true;

            // green area: thumbnail
            PictureBox thumbnail = new PictureBox();
            thumbnail.Left = 5;
            thumbnail.Top = 23;
            thumbnail.Width = 130;
            thumbnail.Height = 60;
            thumbnail.SizeMode = PictureBoxSizeMode.Zoom;
            thumbnail.BorderStyle = BorderStyle.FixedSingle;

            if (!string.IsNullOrWhiteSpace(playlist.ListThumbLink))
            {
                thumbnail.LoadAsync(playlist.ListThumbLink);
            }

            // blue area: item count bottom-right
            Label count = new Label();
            count.Left = 98;
            count.Top = 82;
            count.Width = 37;
            count.Height = 15;
            count.TextAlign = ContentAlignment.MiddleRight;
            count.Text = (playlist.ListVideoCount ?? 0).ToString();

            card.Controls.Add(tick);
            card.Controls.Add(name);
            card.Controls.Add(thumbnail);
            card.Controls.Add(count);

            return card;
        }
        /// <summary>
        /// Retrieves a list of YouTube categories for the specified region and binds them to the category selection
        /// control.
        /// </summary>
        /// <remarks>This method fetches YouTube categories for the region specified in the API call and
        /// sets the data source of the category selection control. The control is configured to display the category
        /// name and use the category ID as the value.</remarks>
        /// <returns></returns>
        private async Task GetYTCategories()
        {
            //full metal clanker
            List<YTCategory> categories =
            await _youtubeAPIController.GetCategories("EE");

            cboxCategory.DisplayMember = "Name";
            cboxCategory.ValueMember = "ID";
            cboxCategory.DataSource = categories;
        }
        /// <summary>
        /// Asynchronously retrieves YouTube categories and populates the category selection dropdown with the results.
        /// </summary>
        /// <remarks>This method fetches a list of YouTube categories using the YouTube API and updates
        /// the dropdown control with the retrieved categories. A default "NONE" option is added to the list, and the
        /// first item is selected by default. The method sets a loading flag during the operation to indicate that the
        /// categories are being loaded.</remarks>
        /// <returns></returns>
        private async Task GetYTCategoriesPrefab()
        {
            _loadingPrefabCategories = true;
            //full metal clanker
            List<YTCategory> categories =
            await _youtubeAPIController.GetCategories("EE");

            cboxCategory_PF.DisplayMember = "Name";
            cboxCategory_PF.ValueMember = "ID";

            List<YTCategory> prefabCategories = new List<YTCategory>();
            prefabCategories.Add(new YTCategory
            {
                ID = "",
                Name = "NONE",
            });
            prefabCategories.AddRange(categories);
            cboxCategory_PF.DataSource = prefabCategories;
            cboxCategory_PF.SelectedIndex = 0;
            _loadingPrefabCategories = false;
        }

        /// <summary>
        /// Handles the <see cref="ComboBox.SelectedIndexChanged"/> event for the privacy selection combo box.
        /// </summary>
        /// <remarks>Displays an informational message if the selected privacy type is <see
        /// cref="PrivacyType.PublishAt"/>,  indicating that scheduling a YouTube video is not currently available. If
        /// the selected privacy type is  <see cref="PrivacyType.Undefined"/>, an error message is displayed, prompting
        /// the user to select a valid option. In both cases, the selection is reset to <see
        /// cref="PrivacyType.Private"/>.</remarks>
        /// <param name="sender">The source of the event, typically the privacy selection combo box.</param>
        /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
        private void cboxPrivacy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((PrivacyType)cboxPrivacy.SelectedItem == PrivacyType.PublishAt)
            {
                string title = "Infos";
                string message = $"Scheduling a scheduled youtube Video is not available at this time.";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboxPrivacy.SelectedItem = PrivacyType.Private;
            }
            if ((PrivacyType)cboxPrivacy.SelectedItem == PrivacyType.Undefined)
            {
                string title = "Error";
                string message = $"Do not leave this underfined when making an entry.";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboxPrivacy.SelectedItem = PrivacyType.Private;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo nav = new ProcessStartInfo()
            {
                FileName = "https://github.com/Estlib/YTA",
                UseShellExecute = true
            };
            Process.Start(nav);
        }
        /// <summary>
        /// Handles the <see cref="ComboBox.SelectedIndexChanged"/> event for the media type selection.
        /// </summary>
        /// <remarks>If the selected media type is <see cref="MediaType.Post"/>, a message box is
        /// displayed to inform the user  that scheduling a YouTube community post is not supported. The selection is
        /// then reverted to <see cref="MediaType.Video"/>.</remarks>
        /// <param name="sender">The source of the event, typically the media type <see cref="ComboBox"/>.</param>
        /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
        private void cboxMediaType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((MediaType)cboxMediaType.SelectedItem == MediaType.Post)
            {
                string title = "Infos";
                string message = $"Scheduling a youtube community post is not supported at this time.";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboxMediaType.SelectedItem = MediaType.Video;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Handles the click event for the "Create Prefab" button, creating a new prefab based on the input fields and
        /// adding it to the prefab collection.
        /// </summary>
        /// <remarks>This method gathers data from the user interface, including text fields, combo boxes,
        /// and checkboxes, to construct a new <see cref="Prefab"/> object. The prefab is then created using the prefab
        /// controller, and the UI is updated to reflect the changes.</remarks>
        /// <param name="sender">The source of the event, typically the button that was clicked.</param>
        /// <param name="e">An instance of <see cref="EventArgs"/> containing event data.</param>
        private void button4_Click(object sender, EventArgs e)
        {
            //is validation needed?
            List<YTPlaylist> selectedLists = FindSelectedLists(fboxLists_PF);
            Prefab newPrefab = new Prefab();
            newPrefab.PrefabName = tboxPrefabName.Text;
            newPrefab.Title = tboxVideoTitle_PF.Text;
            newPrefab.Description = tboxDescription_PF.Text;
            newPrefab.VideoTags = tboxTags_PF.Text;
            newPrefab.ThisMediaIs = (MediaType?)cboxMediaType_PF.SelectedItem;
            if (cboxCategory_PF.SelectedIndex == 0)
            {
                newPrefab.CategoryID = null;
            }
            else
            {
                newPrefab.CategoryID = cboxCategory_PF.SelectedValue.ToString();
            }
            newPrefab.Privacy = (PrivacyType?)cboxPrivacyType_PF.SelectedItem;
            newPrefab.SelfDeclaredMadeForKids = cboxSelfDeclaredMadeForKids_PF.Checked;
            newPrefab.ContainsSyntheticMedia = cboxContainsSyntheticMedia_PF.Checked;
            newPrefab.HasPaidProductPlacement = cboxHasPaidProductPlacement_PF.Checked;
            newPrefab.ListsIds = string.Join(",", selectedLists.Select(x => x.ListID));
            newPrefab.ListsNames = string.Join(",", selectedLists.Select(x => x.ListName));
            _prefabController.Create(newPrefab);
            LoadPrefabs();
            ClearPrefabFields();
        }
        /// <summary>
        /// Handles the <see cref="ComboBox.SelectedIndexChanged"/> event for the media type selection.
        /// </summary>
        /// <remarks>If the selected media type is <see cref="MediaType.Post"/>, a message box is
        /// displayed to inform the user  that scheduling a YouTube community post is not supported. The selection is
        /// then reverted to <see cref="MediaType.Video"/>.</remarks>
        /// <param name="sender">The source of the event, typically the <see cref="ComboBox"/> control.</param>
        /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
        private void cboxMediaType_PF_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((MediaType)cboxMediaType_PF.SelectedItem == MediaType.Post)
            {
                string title = "Infos";
                string message = $"Scheduling a youtube community post is not supported at this time.";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboxMediaType_PF.SelectedItem = MediaType.Video;
            }
        }
        /// <summary>
        /// Handles the <see cref="ComboBox.SelectedIndexChanged"/> event for the privacy type selection.
        /// </summary>
        /// <remarks>This method updates the selected privacy type based on user input and displays
        /// informational or warning messages when specific privacy types are selected. If the user selects <see
        /// cref="PrivacyType.PublishAt"/>, a message is shown indicating that scheduling is not available, and the
        /// selection is reverted to <see cref="PrivacyType.Private"/>.  If the user selects <see
        /// cref="PrivacyType.Undefined"/>, a warning is displayed, and the user is prompted to confirm. Depending on
        /// the user's response, the selection is either kept as <see cref="PrivacyType.Undefined"/> or changed to <see
        /// cref="PrivacyType.Unlisted"/>.</remarks>
        /// <param name="sender">The source of the event, typically the privacy type combo box.</param>
        /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
        private void cboxPrivacyType_PF_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((PrivacyType)cboxPrivacyType_PF.SelectedItem == PrivacyType.PublishAt)
            {
                string title = "Infos";
                string message = $"Scheduling a scheduled youtube Video is not available at this time.";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboxPrivacyType_PF.SelectedItem = PrivacyType.Private;
            }
            if ((PrivacyType)cboxPrivacyType_PF.SelectedItem == PrivacyType.Undefined)
            {
                string title = "Warning";
                string message = $"If left Undefined, the entry will not assign \n" +
                    $"a type, you will then have to manually still\n" +
                    $"define it when scheduling based on this prefab.\n\nProceed?";
                DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    cboxPrivacyType_PF.SelectedItem = PrivacyType.Undefined;
                }
                else
                {
                    string title2 = "Infos";
                    string message2 = $"Setting preference to Unlisted.";
                    var result2 = MessageBox.Show(message2, title2, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboxPrivacyType_PF.SelectedItem = PrivacyType.Unlisted;
                }
            }
        }
        /// <summary>
        /// Handles the <see cref="ComboBox.SelectedIndexChanged"/> event for the category selection ComboBox. Displays
        /// warnings and prompts the user to confirm or adjust their selection when the "NONE" category is chosen.
        /// </summary>
        /// <remarks>If the "NONE" category is selected, the user is warned that no category will be
        /// assigned, and they are prompted to confirm or select a different category. The method does not proceed if
        /// the category list is being loaded programmatically.</remarks>
        /// <param name="sender">The source of the event, typically the category selection ComboBox.</param>
        /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
        private void cboxCategory_PF_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loadingPrefabCategories)
            {
                return;
            }

            if (cboxCategory_PF.SelectedIndex == 0)
            {
                string title = "Warning";
                string message = $"If left NONE, the entry will not assign \n" +
                    $"a category, you will then have to manually still\n" +
                    $"define it when scheduling based on this prefab.\n\nProceed?";
                DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    cboxCategory_PF.SelectedIndex = 0;
                }
                else
                {
                    string title2 = "Infos";
                    string message2 = $"Please select a desired category then.";
                    var result2 = MessageBox.Show(message2, title2, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboxCategory_PF.SelectedIndex = 1;
                }
            }
        }
        /// <summary>
        /// Handles the click event for the "Update Prefab" button, updating the currently selected prefab with the
        /// provided details.
        /// </summary>
        /// <remarks>This method updates the prefab currently being edited with the values entered in the
        /// form fields.  If no prefab is selected, a message box is displayed to inform the user. The updated prefab
        /// details  include its name, title, description, tags, media type, category, privacy settings, and other
        /// metadata. After updating, the prefab list is reloaded, and the form fields are cleared.</remarks>
        /// <param name="sender">The source of the event, typically the button that was clicked.</param>
        /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnUpdatePrefab_Click(object sender, EventArgs e)
        {
            if (_currentlyEditingPrefabID == null)
            {
                string title2 = "Infos";
                string message2 = $"Please select a prefab to edit.";
                var result2 = MessageBox.Show(message2, title2, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<YTPlaylist> selectedLists = FindSelectedLists(fboxLists_PF);
            Prefab changedPrefab = new Prefab();
            changedPrefab.PrefabName = tboxVideoTitle_PF.Text;
            changedPrefab.Title = tboxVideoTitle_PF.Text;
            changedPrefab.Description = tboxDescription_PF.Text;
            changedPrefab.VideoTags = tboxTags_PF.Text;
            changedPrefab.ThisMediaIs = (MediaType?)cboxMediaType_PF.SelectedItem;
            if (cboxCategory_PF.SelectedIndex == 0)
            {
                changedPrefab.CategoryID = null;
            }
            else
            {
                if (cboxCategory_PF.SelectedValue != null)
                {
                    changedPrefab.CategoryID = cboxCategory_PF.SelectedValue.ToString();
                }
                else
                {
                    changedPrefab.CategoryID = null;
                }
            }
            changedPrefab.Privacy = (PrivacyType?)cboxPrivacyType_PF.SelectedItem;
            changedPrefab.SelfDeclaredMadeForKids = cboxSelfDeclaredMadeForKids_PF.Checked;
            changedPrefab.ContainsSyntheticMedia = cboxContainsSyntheticMedia_PF.Checked;
            changedPrefab.HasPaidProductPlacement = cboxHasPaidProductPlacement_PF.Checked;
            changedPrefab.ListsIds = string.Join(",", selectedLists.Select(x => x.ListID));
            changedPrefab.ListsNames = string.Join(",", selectedLists.Select(x => x.ListName));
            _prefabController.Update(changedPrefab, (Guid)_currentlyEditingPrefabID);
            LoadPrefabs();
            ClearPrefabFields();
        }
        /// <summary>
        /// Handles the event triggered when the selected index of the prefab combo box changes.
        /// </summary>
        /// <remarks>This method updates the form data based on the selected prefab. If the combo box is
        /// in a loading state  or the selected value is null or empty, the method exits without performing any action.
        /// If the selected  prefab cannot be retrieved, an error message is displayed to the user.</remarks>
        /// <param name="sender">The source of the event, typically the combo box.</param>
        /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
        private void cboxWhichPrefab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loadingPrefabs == true || cboxWhichPrefab.SelectedValue == null)
            {
                return;
            }
            Guid isItEmpty = (Guid)cboxWhichPrefab.SelectedValue;
            if (isItEmpty == Guid.Empty)
            {
                return;
            }
            Prefab? prefab = _prefabController.GetOneByID(isItEmpty);
            if (prefab == null)
            {
                string title = "Error";
                string message = $"Cannot get prefab, ID {isItEmpty.ToString()} is null.";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (prefab != null)
            {
                EntryFormData(prefab);
            }
        }
        /// <summary>
        /// Populates the form fields with data from the specified <see cref="Prefab"/> instance.
        /// </summary>
        /// <remarks>This method updates various form controls, such as combo boxes, text boxes, and
        /// checkboxes,  based on the properties of the provided <see cref="Prefab"/> object.  Only non-null properties
        /// of the <paramref name="prefab"/> object are applied to the form.</remarks>
        /// <param name="prefab">The <see cref="Prefab"/> instance containing the data to populate the form.  Null or missing values in the
        /// <paramref name="prefab"/> object will leave the corresponding form fields unchanged.</param>
        private void EntryFormData(Prefab prefab)
        {
            if (prefab.ThisMediaIs != null)
            {
                cboxMediaType.SelectedItem = prefab.ThisMediaIs.Value;
            }
            if (prefab.Privacy != null)
            {
                cboxPrivacy.SelectedItem = prefab.Privacy.Value;
            }
            if (prefab.Title != null)
            {
                tboxTitle.Text = prefab.Title;
            }
            if (prefab.Description != null)
            {
                tboxDescription.Text = prefab.Description;
            }
            if (prefab.VideoTags != null)
            {
                tboxTags.Text = prefab.VideoTags;
            }
            if (prefab.CategoryID != null)
            {
                cboxCategory.SelectedValue = prefab.CategoryID;
            }
            if (prefab.SelfDeclaredMadeForKids != null)
            {
                tickChild.Checked = prefab.SelfDeclaredMadeForKids.Value;
            }
            if (prefab.ContainsSyntheticMedia != null)
            {
                tickRobot.Checked = prefab.ContainsSyntheticMedia.Value;
            }
            if (prefab.HasPaidProductPlacement != null)
            {
                tickProduct.Checked = prefab.HasPaidProductPlacement.Value;
            }
            if (prefab.ListsIds != null)
            {
                TickListBoxes(fboxPlaylists, prefab.ListsIds);
            }
        }
        /// <summary>
        /// Populates the entry form fields with data from the specified <see cref="Prefab"/> object.
        /// </summary>
        /// <remarks>This method updates various form controls, such as text boxes, combo boxes, and
        /// checkboxes, to reflect the values provided in the <paramref name="prefab"/> object. Default values are
        /// applied for fields where the corresponding <paramref name="prefab"/> property is null.</remarks>
        /// <param name="prefab">The <see cref="Prefab"/> object containing the data to populate the form fields. If a property of the
        /// <paramref name="prefab"/> is null, a default value will be used where applicable.</param>
        private void EntryFormDataPFEdit(Prefab prefab)
        {
            if (prefab.PrefabName != null)
            {
                tboxPrefabName.Text = prefab.PrefabName;
            }
            else
            {
                tboxPrefabName.Text = "Untitled prefab";
            }
            if (prefab.ThisMediaIs != null)
            {
                cboxMediaType_PF.SelectedItem = prefab.ThisMediaIs.Value;
            }
            if (prefab.Privacy != null)
            {
                cboxPrivacyType_PF.SelectedItem = prefab.Privacy.Value;
            }
            if (prefab.Title != null)
            {
                tboxVideoTitle_PF.Text = prefab.Title;
            }
            if (prefab.Description != null)
            {
                tboxDescription_PF.Text = prefab.Description;
            }
            if (prefab.VideoTags != null)
            {
                tboxTags_PF.Text = prefab.VideoTags;
            }
            if (prefab.CategoryID != null)
            {
                cboxCategory_PF.SelectedValue = prefab.CategoryID;
            }
            if (prefab.SelfDeclaredMadeForKids != null)
            {
                cboxSelfDeclaredMadeForKids_PF.Checked = prefab.SelfDeclaredMadeForKids.Value;
            }
            if (prefab.ContainsSyntheticMedia != null)
            {
                cboxContainsSyntheticMedia_PF.Checked = prefab.ContainsSyntheticMedia.Value;
            }
            if (prefab.HasPaidProductPlacement != null)
            {
                cboxHasPaidProductPlacement_PF.Checked = prefab.HasPaidProductPlacement.Value;
            }
            if (prefab.ListsIds != null)
            {
                TickListBoxes(fboxLists_PF, prefab.ListsIds);
            }
        }
        /// <summary>
        /// Updates the checked state of <see cref="CheckBox"/> controls within the specified <see
        /// cref="FlowLayoutPanel"/> based on a comma-separated list of playlist IDs.
        /// </summary>
        /// <remarks>This method assumes that the <see cref="CheckBox.Tag"/> property is of type
        /// <c>YTPlaylist</c> and that the <c>YTPlaylist</c> object contains a valid <c>ListID</c>. If these assumptions
        /// are not met, the behavior is undefined.</remarks>
        /// <param name="panel">The <see cref="FlowLayoutPanel"/> containing the controls to update. Each control is expected to contain
        /// child <see cref="CheckBox"/> controls with a <see cref="Tag"/> property of type <c>YTPlaylist</c>.</param>
        /// <param name="listsIds">A comma-separated string of playlist IDs. Each ID corresponds to a <see cref="YTPlaylist.ListID"/>
        /// associated with a <see cref="CheckBox"/>. If the ID is present in this string, the corresponding <see
        /// cref="CheckBox"/> will be checked; otherwise, it will be unchecked. Can be null or empty, in which case all
        /// checkboxes will be unchecked.</param>
        private void TickListBoxes(FlowLayoutPanel panel, string listsIds)
        {
            //full clanker code bcs am slow
            List<string> selectedIDs = new List<string>();
            if (!string.IsNullOrWhiteSpace(listsIds))
            {
                selectedIDs = listsIds
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .ToList();
            }

            foreach (Control card in panel.Controls)
            {
                foreach (Control child in card.Controls)
                {
                    if (child is CheckBox tick && tick.Tag is YTPlaylist playlist)
                    {
                        tick.Checked = selectedIDs.Contains(playlist.ListID);
                    }
                }
            }
        }
        /// <summary>
        /// Handles the event triggered when the selected index of the prefab combo box changes.
        /// </summary>
        /// <remarks>This method updates the currently selected prefab based on the new selection in the
        /// combo box. If the selection is empty or invalid, the method resets the current prefab state. Displays an
        /// error message if the selected prefab cannot be retrieved.</remarks>
        /// <param name="sender">The source of the event, typically the combo box.</param>
        /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
        private void cboxWhichPrefab_PF_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loadingPrefabs == true || cboxWhichPrefab_PF.SelectedValue == null)
            {
                return;
            }
            Guid isItEmpty = (Guid)cboxWhichPrefab_PF.SelectedValue;
            if (isItEmpty == Guid.Empty)
            {
                _currentlyEditingPrefabID = null;
                return;
            }
            Prefab? prefab = _prefabController.GetOneByID(isItEmpty);
            if (prefab == null)
            {
                string title = "Error";
                string message = $"Cannot get prefab, ID {isItEmpty.ToString()} is null.";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (prefab != null)
            {
                _currentlyEditingPrefabID = prefab.ID;
                EntryFormDataPFEdit(prefab);
            }
        }
        /// <summary>
        /// Handles the click event for the Delete Prefab button. Prompts the user for confirmation before deleting the
        /// currently selected prefab.
        /// </summary>
        /// <remarks>If no prefab is currently selected, an informational message is displayed to the
        /// user. If a prefab is selected, the user is prompted with a confirmation dialog. Upon confirmation, the
        /// selected prefab is deleted, and the list of prefabs is reloaded.</remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnDeletePrefab_Click(object sender, EventArgs e)
        {
            if (_currentlyEditingPrefabID == null)
            {
                string title2 = "Error";
                string message2 = $"No id.";
                var result2 = MessageBox.Show(message2, title2, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                string title = "Warning";
                string message = $"Are you sure about that?";
                DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    _prefabController.Delete((Guid)_currentlyEditingPrefabID);
                }
                else
                {
                    return;
                }

            }
            LoadPrefabs();
            ClearPrefabFields();
        }
        /// <summary>
        /// Resets all fields in the prefab editing form to their default values.
        /// </summary>
        /// <remarks>This method clears text fields, resets combo box selections to their first item, 
        /// unchecks all checkboxes, and removes any specific prefab ID currently being edited. It ensures the form is
        /// in a clean state, ready for new input.</remarks>
        private void ClearPrefabFields()
        {
            _currentlyEditingPrefabID = null;

            tboxPrefabName.Text = "";
            tboxVideoTitle_PF.Text = "";
            tboxDescription_PF.Text = "";
            tboxTags_PF.Text = "";

            if (cboxMediaType_PF.Items.Count > 0)
            {
                cboxMediaType_PF.SelectedIndex = 0;
            }

            if (cboxPrivacyType_PF.Items.Count > 0)
            {
                cboxPrivacyType_PF.SelectedIndex = 0;
            }

            if (cboxCategory_PF.Items.Count > 0)
            {
                cboxCategory_PF.SelectedIndex = 0; // NONE
            }

            cboxSelfDeclaredMadeForKids_PF.Checked = false;
            cboxContainsSyntheticMedia_PF.Checked = false;
            cboxHasPaidProductPlacement_PF.Checked = false;

            foreach (Control card in fboxLists_PF.Controls)
            {
                foreach (Control child in card.Controls)
                {
                    if (child is CheckBox tick)
                    {
                        tick.Checked = false;
                    }
                }
            }
        }

        private void tickBubbles_CheckedChanged(object sender, EventArgs e)
        {
            if (tickBubbles.Checked)
            {
                tickBubbles.Text = "Messages from tray";
                _OSMSGEnable = true;
            }
            else
            {
                tickBubbles.Text = "Silenced";
                _OSMSGEnable = false;
            }
        }

        private void tickReminders_CheckedChanged(object sender, EventArgs e)
        {
            if (tickReminders.Checked)
            {
                tickReminders.Text = "Will remind after upload.";
                _reminders = true;
            }
            else
            {
                tickReminders.Text = "Reminders off.";
                _reminders = false;
            }
        }
    }
}
