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

        private DateTime _lastScanTime = DateTime.Now;
        private DateTime _nextScanTime = DateTime.Now;
        private bool _tickerBusy = false;

        public Form1()
        {

            InitializeComponent();
            SetupComboEnum();
            SetupTicker();
        }

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

        private void UpdateScanInfo()
        {
            tboxWhenLastScan.Text = _lastScanTime.ToString();
            tboxNextScanTime.Text = _nextScanTime.ToString();
            tboxHowManyUploaded.Text = _contentController.CountUploaded().ToString();
        }

        // form element methods
        private void SetupComboEnum()
        {
            cboxMediaType.DataSource = Enum.GetValues(typeof(MediaType));
            cboxPrivacy.DataSource = Enum.GetValues(typeof(PrivacyType));
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnRefreshData_Click(object sender, EventArgs e)
        {
            GetDataFromDB();
        }
        private void btnAddEntry_Click(object sender, EventArgs e)
        {
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
                GetYTCategories();
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
            else if(tboxDescription.Text.Count() > 5000)
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
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

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
                List<YTContent> toUpload = _contentController.GetUploadQueue(_lastScanTime, thisScanTime);
                LogTool($"Found {toUpload.Count} in queue");
                foreach (var queueEntry in toUpload)
                {
                    LogTool($"Uploading \"{queueEntry.Title}\"");
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
                }
                _lastScanTime = thisScanTime;
                _nextScanTime = DateTime.Now.AddMilliseconds(uploadTicker.Interval);
                LogTool("Cycle finished.");

            }
            catch (Exception ex)
            {
                LogTool($"Error, see details:\n\n{ex.Message}");
            }
            finally
            {
                _tickerBusy = false;

                UpdateScanInfo();
                GetDataFromDB();
            }
        }

        private void LogTool(string v)
        {
            tboxTickerLog.AppendText($"[{DateTime.Now.ToString("M HH:mm:ss")}] - " + v + Environment.NewLine);

        }

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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == calendarTab)
            {
                RefreshCalendarData();
            }
            if (tabControl1.SelectedTab == entriesTab)
            {
                GetDataFromDB();
                GetYTCategories();
            }
        }

        private async void GetYTCategories()
        {
            //full metal clanker
            List<YTcategory> categories =
            await _youtubeAPIController.GetCategories("EE");

            cboxCategory.DisplayMember = "Name";
            cboxCategory.ValueMember = "ID";
            cboxCategory.DataSource = categories;
        }

        private void tboxVideopath_TextChanged(object sender, EventArgs e)
        {

        }

        private void tboxDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboxPrivacy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((PrivacyType)cboxPrivacy.SelectedItem == PrivacyType.PublishAt)
            {
                string title = "Infos";
                string message = $"Scheduling a scheduled youtube Video is not available at this time.";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboxPrivacy.SelectedItem = PrivacyType.Private;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo nav = new ProcessStartInfo() 
            {FileName= "https://github.com/Estlib/YTA",
            UseShellExecute = true};
            Process.Start(nav);
        }

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
    }
}
