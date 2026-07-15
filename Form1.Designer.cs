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
namespace YTA
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            tabControl1 = new TabControl();
            loginTab = new TabPage();
            groupBox3 = new GroupBox();
            linkLabel1 = new LinkLabel();
            label15 = new Label();
            textBox1 = new TextBox();
            loggedInInfo = new GroupBox();
            channelViewCount = new Label();
            label5 = new Label();
            channelTitleBig = new Label();
            videoCount = new Label();
            label4 = new Label();
            subCount = new Label();
            label3 = new Label();
            avatarBigImage = new PictureBox();
            labelChannelName = new Label();
            labelIsLoggedIn = new Label();
            btnLogOut = new Button();
            btnLogin = new Button();
            calendarTab = new TabPage();
            btnRefreshCalendar = new Button();
            scheduledEntries = new FlowLayoutPanel();
            label1 = new Label();
            scheduleCalendar = new MonthCalendar();
            newEntryTab = new TabPage();
            label18 = new Label();
            cboxWhichPrefab = new ComboBox();
            label17 = new Label();
            fboxPlaylists = new FlowLayoutPanel();
            cboxCategory = new ComboBox();
            btnAddEntry = new Button();
            dateTimePicker1 = new DateTimePicker();
            tickProduct = new CheckBox();
            tickRobot = new CheckBox();
            tickChild = new CheckBox();
            cboxPrivacy = new ComboBox();
            label2 = new Label();
            tboxTags = new TextBox();
            tboxDescription = new TextBox();
            tboxTitle = new TextBox();
            btnImageBrowser = new Button();
            tboxImagepath = new TextBox();
            btnFileBrowser = new Button();
            tboxVideopath = new TextBox();
            cboxMediaType = new ComboBox();
            prefabTab = new TabPage();
            tabControl2 = new TabControl();
            infoTab = new TabPage();
            richTextBox2 = new RichTextBox();
            newPrefab = new TabPage();
            btnDeletePrefab = new Button();
            textBox5 = new TextBox();
            label22 = new Label();
            btnUpdatePrefab = new Button();
            btnCreateUpdate_PF = new Button();
            tboxTags_PF = new TextBox();
            tboxDescription_PF = new TextBox();
            cboxHasPaidProductPlacement_PF = new CheckBox();
            cboxContainsSyntheticMedia_PF = new CheckBox();
            cboxSelfDeclaredMadeForKids_PF = new CheckBox();
            cboxCategory_PF = new ComboBox();
            label21 = new Label();
            tboxVideoTitle_PF = new TextBox();
            label20 = new Label();
            cboxWhichPrefab_PF = new ComboBox();
            label19 = new Label();
            fboxLists_PF = new FlowLayoutPanel();
            cboxPrivacyType_PF = new ComboBox();
            cboxMediaType_PF = new ComboBox();
            managePrefabTab = new TabPage();
            entriesTab = new TabPage();
            btnDeleteRow = new Button();
            btnUploadNow = new Button();
            btnCopyLink = new Button();
            btnNavigate = new Button();
            btnRefreshData = new Button();
            dataEntryDisplay = new DataGridView();
            tickerConf = new TabPage();
            groupBox2 = new GroupBox();
            label11 = new Label();
            tboxHowManyUploaded = new TextBox();
            label10 = new Label();
            tboxWhenLastScan = new TextBox();
            tboxNextScanTime = new TextBox();
            label7 = new Label();
            groupBox1 = new GroupBox();
            label16 = new Label();
            checkBox1 = new CheckBox();
            button3 = new Button();
            button2 = new Button();
            label14 = new Label();
            tickTrayMinimizeSetting = new CheckBox();
            label13 = new Label();
            checkBox3 = new CheckBox();
            button1 = new Button();
            label12 = new Label();
            checkBox2 = new CheckBox();
            label6 = new Label();
            tickerIntervalSetting = new NumericUpDown();
            label8 = new Label();
            tickerOnOff = new CheckBox();
            label9 = new Label();
            tboxTickerLog = new TextBox();
            aboutTab = new TabPage();
            richTextBox1 = new RichTextBox();
            uploadTicker = new System.Windows.Forms.Timer(components);
            trayIcon = new NotifyIcon(components);
            tabControl1.SuspendLayout();
            loginTab.SuspendLayout();
            groupBox3.SuspendLayout();
            loggedInInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)avatarBigImage).BeginInit();
            calendarTab.SuspendLayout();
            newEntryTab.SuspendLayout();
            prefabTab.SuspendLayout();
            tabControl2.SuspendLayout();
            infoTab.SuspendLayout();
            newPrefab.SuspendLayout();
            entriesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataEntryDisplay).BeginInit();
            tickerConf.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tickerIntervalSetting).BeginInit();
            aboutTab.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(loginTab);
            tabControl1.Controls.Add(calendarTab);
            tabControl1.Controls.Add(newEntryTab);
            tabControl1.Controls.Add(prefabTab);
            tabControl1.Controls.Add(entriesTab);
            tabControl1.Controls.Add(tickerConf);
            tabControl1.Controls.Add(aboutTab);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(776, 426);
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // loginTab
            // 
            loginTab.Controls.Add(groupBox3);
            loginTab.Controls.Add(loggedInInfo);
            loginTab.Controls.Add(labelChannelName);
            loginTab.Controls.Add(labelIsLoggedIn);
            loginTab.Controls.Add(btnLogOut);
            loginTab.Controls.Add(btnLogin);
            loginTab.Location = new Point(4, 24);
            loginTab.Name = "loginTab";
            loginTab.Padding = new Padding(3);
            loginTab.Size = new Size(768, 398);
            loginTab.TabIndex = 0;
            loginTab.Text = "Log In";
            loginTab.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(linkLabel1);
            groupBox3.Controls.Add(label15);
            groupBox3.Controls.Add(textBox1);
            groupBox3.Location = new Point(6, 76);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(306, 316);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            groupBox3.Text = "Feature notes";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(240, 298);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(62, 15);
            linkLabel1.TabIndex = 3;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Estlib 2026";
            linkLabel1.TextAlign = ContentAlignment.TopRight;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(6, 298);
            label15.Name = "label15";
            label15.Size = new Size(79, 15);
            label15.TabIndex = 1;
            label15.Text = "Version: V1.03";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(6, 15);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(294, 280);
            textBox1.TabIndex = 0;
            textBox1.Text = resources.GetString("textBox1.Text");
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // loggedInInfo
            // 
            loggedInInfo.Controls.Add(channelViewCount);
            loggedInInfo.Controls.Add(label5);
            loggedInInfo.Controls.Add(channelTitleBig);
            loggedInInfo.Controls.Add(videoCount);
            loggedInInfo.Controls.Add(label4);
            loggedInInfo.Controls.Add(subCount);
            loggedInInfo.Controls.Add(label3);
            loggedInInfo.Controls.Add(avatarBigImage);
            loggedInInfo.Location = new Point(318, 6);
            loggedInInfo.Name = "loggedInInfo";
            loggedInInfo.Size = new Size(444, 386);
            loggedInInfo.TabIndex = 5;
            loggedInInfo.TabStop = false;
            loggedInInfo.Text = "User info";
            // 
            // channelViewCount
            // 
            channelViewCount.AutoSize = true;
            channelViewCount.Location = new Point(400, 100);
            channelViewCount.Name = "channelViewCount";
            channelViewCount.Size = new Size(38, 15);
            channelViewCount.TabIndex = 7;
            channelViewCount.Text = "count";
            channelViewCount.TextAlign = ContentAlignment.TopRight;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(262, 100);
            label5.Name = "label5";
            label5.Size = new Size(84, 15);
            label5.TabIndex = 6;
            label5.Text = "Channel Views";
            // 
            // channelTitleBig
            // 
            channelTitleBig.AutoSize = true;
            channelTitleBig.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 186);
            channelTitleBig.Location = new Point(6, 19);
            channelTitleBig.Name = "channelTitleBig";
            channelTitleBig.Size = new Size(175, 37);
            channelTitleBig.TabIndex = 5;
            channelTitleBig.Text = "channelname";
            // 
            // videoCount
            // 
            videoCount.AutoSize = true;
            videoCount.Location = new Point(400, 85);
            videoCount.Name = "videoCount";
            videoCount.Size = new Size(38, 15);
            videoCount.TabIndex = 4;
            videoCount.Text = "count";
            videoCount.TextAlign = ContentAlignment.TopRight;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(262, 85);
            label4.Name = "label4";
            label4.Size = new Size(42, 15);
            label4.TabIndex = 3;
            label4.Text = "Videos";
            // 
            // subCount
            // 
            subCount.AutoSize = true;
            subCount.Location = new Point(400, 70);
            subCount.Name = "subCount";
            subCount.Size = new Size(38, 15);
            subCount.TabIndex = 2;
            subCount.Text = "count";
            subCount.TextAlign = ContentAlignment.TopRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(262, 70);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 1;
            label3.Text = "Subscribers";
            // 
            // avatarBigImage
            // 
            avatarBigImage.Location = new Point(6, 70);
            avatarBigImage.Name = "avatarBigImage";
            avatarBigImage.Size = new Size(250, 250);
            avatarBigImage.SizeMode = PictureBoxSizeMode.StretchImage;
            avatarBigImage.TabIndex = 0;
            avatarBigImage.TabStop = false;
            // 
            // labelChannelName
            // 
            labelChannelName.AutoSize = true;
            labelChannelName.BorderStyle = BorderStyle.FixedSingle;
            labelChannelName.Location = new Point(6, 49);
            labelChannelName.Name = "labelChannelName";
            labelChannelName.Size = new Size(88, 17);
            labelChannelName.TabIndex = 4;
            labelChannelName.Text = "Channel Name";
            // 
            // labelIsLoggedIn
            // 
            labelIsLoggedIn.AutoSize = true;
            labelIsLoggedIn.BorderStyle = BorderStyle.FixedSingle;
            labelIsLoggedIn.Location = new Point(6, 32);
            labelIsLoggedIn.Name = "labelIsLoggedIn";
            labelIsLoggedIn.Size = new Size(72, 17);
            labelIsLoggedIn.TabIndex = 2;
            labelIsLoggedIn.Text = "IsLoggedIn?";
            // 
            // btnLogOut
            // 
            btnLogOut.Location = new Point(87, 6);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Size = new Size(75, 23);
            btnLogOut.TabIndex = 1;
            btnLogOut.Text = "Log Out";
            btnLogOut.UseVisualStyleBackColor = true;
            btnLogOut.Click += btnLogOut_Click;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(6, 6);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(75, 23);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "Log In";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // calendarTab
            // 
            calendarTab.Controls.Add(btnRefreshCalendar);
            calendarTab.Controls.Add(scheduledEntries);
            calendarTab.Controls.Add(label1);
            calendarTab.Controls.Add(scheduleCalendar);
            calendarTab.Location = new Point(4, 24);
            calendarTab.Name = "calendarTab";
            calendarTab.Padding = new Padding(3);
            calendarTab.Size = new Size(768, 398);
            calendarTab.TabIndex = 1;
            calendarTab.Text = "Calendar";
            calendarTab.UseVisualStyleBackColor = true;
            // 
            // btnRefreshCalendar
            // 
            btnRefreshCalendar.Location = new Point(9, 369);
            btnRefreshCalendar.Name = "btnRefreshCalendar";
            btnRefreshCalendar.Size = new Size(157, 23);
            btnRefreshCalendar.TabIndex = 4;
            btnRefreshCalendar.Text = "Refresh";
            btnRefreshCalendar.UseVisualStyleBackColor = true;
            btnRefreshCalendar.Click += btnRefreshCalendar_Click;
            // 
            // scheduledEntries
            // 
            scheduledEntries.AutoScroll = true;
            scheduledEntries.Location = new Point(172, 6);
            scheduledEntries.Name = "scheduledEntries";
            scheduledEntries.Size = new Size(590, 386);
            scheduledEntries.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 6);
            label1.Name = "label1";
            label1.Size = new Size(63, 15);
            label1.TabIndex = 1;
            label1.Text = "Select day:";
            // 
            // scheduleCalendar
            // 
            scheduleCalendar.CalendarDimensions = new Size(1, 2);
            scheduleCalendar.Location = new Point(9, 27);
            scheduleCalendar.Name = "scheduleCalendar";
            scheduleCalendar.TabIndex = 0;
            scheduleCalendar.DateChanged += scheduleCalendar_DateChanged;
            // 
            // newEntryTab
            // 
            newEntryTab.Controls.Add(label18);
            newEntryTab.Controls.Add(cboxWhichPrefab);
            newEntryTab.Controls.Add(label17);
            newEntryTab.Controls.Add(fboxPlaylists);
            newEntryTab.Controls.Add(cboxCategory);
            newEntryTab.Controls.Add(btnAddEntry);
            newEntryTab.Controls.Add(dateTimePicker1);
            newEntryTab.Controls.Add(tickProduct);
            newEntryTab.Controls.Add(tickRobot);
            newEntryTab.Controls.Add(tickChild);
            newEntryTab.Controls.Add(cboxPrivacy);
            newEntryTab.Controls.Add(label2);
            newEntryTab.Controls.Add(tboxTags);
            newEntryTab.Controls.Add(tboxDescription);
            newEntryTab.Controls.Add(tboxTitle);
            newEntryTab.Controls.Add(btnImageBrowser);
            newEntryTab.Controls.Add(tboxImagepath);
            newEntryTab.Controls.Add(btnFileBrowser);
            newEntryTab.Controls.Add(tboxVideopath);
            newEntryTab.Controls.Add(cboxMediaType);
            newEntryTab.Location = new Point(4, 24);
            newEntryTab.Name = "newEntryTab";
            newEntryTab.Size = new Size(768, 398);
            newEntryTab.TabIndex = 2;
            newEntryTab.Text = "Add entry";
            newEntryTab.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(405, 6);
            label18.Name = "label18";
            label18.Size = new Size(78, 15);
            label18.TabIndex = 23;
            label18.Text = "Select Prefab:";
            // 
            // cboxWhichPrefab
            // 
            cboxWhichPrefab.FormattingEnabled = true;
            cboxWhichPrefab.Location = new Point(493, 3);
            cboxWhichPrefab.Name = "cboxWhichPrefab";
            cboxWhichPrefab.Size = new Size(272, 23);
            cboxWhichPrefab.TabIndex = 22;
            cboxWhichPrefab.SelectedIndexChanged += cboxWhichPrefab_SelectedIndexChanged;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(298, 6);
            label17.Name = "label17";
            label17.Size = new Size(101, 15);
            label17.TabIndex = 19;
            label17.Text = "Add to playlists ▼";
            // 
            // fboxPlaylists
            // 
            fboxPlaylists.AutoScroll = true;
            fboxPlaylists.Location = new Point(298, 32);
            fboxPlaylists.Name = "fboxPlaylists";
            fboxPlaylists.Size = new Size(467, 110);
            fboxPlaylists.TabIndex = 17;
            // 
            // cboxCategory
            // 
            cboxCategory.FormattingEnabled = true;
            cboxCategory.Location = new Point(80, 119);
            cboxCategory.Name = "cboxCategory";
            cboxCategory.Size = new Size(212, 23);
            cboxCategory.TabIndex = 16;
            cboxCategory.SelectedIndexChanged += cboxCategory_SelectedIndexChanged;
            // 
            // btnAddEntry
            // 
            btnAddEntry.Location = new Point(690, 372);
            btnAddEntry.Name = "btnAddEntry";
            btnAddEntry.Size = new Size(75, 23);
            btnAddEntry.TabIndex = 15;
            btnAddEntry.Text = "Add Entry";
            btnAddEntry.UseVisualStyleBackColor = true;
            btnAddEntry.Click += btnAddEntry_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(388, 300);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 14;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // tickProduct
            // 
            tickProduct.AutoSize = true;
            tickProduct.Location = new Point(594, 340);
            tickProduct.Name = "tickProduct";
            tickProduct.Size = new Size(167, 19);
            tickProduct.TabIndex = 13;
            tickProduct.Text = "Is product placed in video?";
            tickProduct.UseVisualStyleBackColor = true;
            // 
            // tickRobot
            // 
            tickRobot.AutoSize = true;
            tickRobot.Location = new Point(594, 320);
            tickRobot.Name = "tickRobot";
            tickRobot.Size = new Size(129, 19);
            tickRobot.TabIndex = 12;
            tickRobot.Text = "Is made by clanker?";
            tickRobot.UseVisualStyleBackColor = true;
            // 
            // tickChild
            // 
            tickChild.AutoSize = true;
            tickChild.Location = new Point(594, 300);
            tickChild.Name = "tickChild";
            tickChild.Size = new Size(81, 19);
            tickChild.TabIndex = 11;
            tickChild.Text = "Is for kids?";
            tickChild.UseVisualStyleBackColor = true;
            // 
            // cboxPrivacy
            // 
            cboxPrivacy.FormattingEnabled = true;
            cboxPrivacy.Location = new Point(130, 3);
            cboxPrivacy.Name = "cboxPrivacy";
            cboxPrivacy.Size = new Size(162, 23);
            cboxPrivacy.TabIndex = 10;
            cboxPrivacy.Text = "Set video privacy";
            cboxPrivacy.SelectedIndexChanged += cboxPrivacy_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 122);
            label2.Name = "label2";
            label2.Size = new Size(71, 15);
            label2.TabIndex = 8;
            label2.Text = "Category id:";
            label2.Click += label2_Click;
            // 
            // tboxTags
            // 
            tboxTags.Location = new Point(3, 300);
            tboxTags.Multiline = true;
            tboxTags.Name = "tboxTags";
            tboxTags.PlaceholderText = "video tags, separate with comma, space is not a separator";
            tboxTags.ScrollBars = ScrollBars.Vertical;
            tboxTags.Size = new Size(378, 95);
            tboxTags.TabIndex = 7;
            // 
            // tboxDescription
            // 
            tboxDescription.AcceptsReturn = true;
            tboxDescription.Location = new Point(3, 148);
            tboxDescription.Multiline = true;
            tboxDescription.Name = "tboxDescription";
            tboxDescription.PlaceholderText = "explain what you've done";
            tboxDescription.ScrollBars = ScrollBars.Vertical;
            tboxDescription.Size = new Size(762, 146);
            tboxDescription.TabIndex = 6;
            tboxDescription.TextChanged += tboxDescription_TextChanged;
            // 
            // tboxTitle
            // 
            tboxTitle.Location = new Point(3, 90);
            tboxTitle.Name = "tboxTitle";
            tboxTitle.PlaceholderText = "Video title";
            tboxTitle.Size = new Size(289, 23);
            tboxTitle.TabIndex = 5;
            // 
            // btnImageBrowser
            // 
            btnImageBrowser.Location = new Point(201, 60);
            btnImageBrowser.Name = "btnImageBrowser";
            btnImageBrowser.Size = new Size(91, 23);
            btnImageBrowser.TabIndex = 4;
            btnImageBrowser.Text = "Where image";
            btnImageBrowser.UseVisualStyleBackColor = true;
            btnImageBrowser.Click += btnImageBrowser_Click;
            // 
            // tboxImagepath
            // 
            tboxImagepath.Location = new Point(3, 61);
            tboxImagepath.Name = "tboxImagepath";
            tboxImagepath.PlaceholderText = "Thumbnail location on disk";
            tboxImagepath.Size = new Size(192, 23);
            tboxImagepath.TabIndex = 3;
            // 
            // btnFileBrowser
            // 
            btnFileBrowser.Location = new Point(201, 32);
            btnFileBrowser.Name = "btnFileBrowser";
            btnFileBrowser.Size = new Size(91, 23);
            btnFileBrowser.TabIndex = 2;
            btnFileBrowser.Text = "Where";
            btnFileBrowser.UseVisualStyleBackColor = true;
            btnFileBrowser.Click += btnFileBrowser_Click;
            // 
            // tboxVideopath
            // 
            tboxVideopath.Location = new Point(3, 32);
            tboxVideopath.Name = "tboxVideopath";
            tboxVideopath.PlaceholderText = "Video location on disk";
            tboxVideopath.Size = new Size(192, 23);
            tboxVideopath.TabIndex = 1;
            tboxVideopath.TextChanged += tboxVideopath_TextChanged;
            // 
            // cboxMediaType
            // 
            cboxMediaType.FormattingEnabled = true;
            cboxMediaType.Location = new Point(3, 3);
            cboxMediaType.Name = "cboxMediaType";
            cboxMediaType.Size = new Size(121, 23);
            cboxMediaType.TabIndex = 0;
            cboxMediaType.Text = "Select media type";
            cboxMediaType.SelectedIndexChanged += cboxMediaType_SelectedIndexChanged;
            // 
            // prefabTab
            // 
            prefabTab.Controls.Add(tabControl2);
            prefabTab.Location = new Point(4, 24);
            prefabTab.Name = "prefabTab";
            prefabTab.Size = new Size(768, 398);
            prefabTab.TabIndex = 6;
            prefabTab.Text = "Entry Prefabs";
            prefabTab.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(infoTab);
            tabControl2.Controls.Add(newPrefab);
            tabControl2.Controls.Add(managePrefabTab);
            tabControl2.Location = new Point(3, 3);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(762, 392);
            tabControl2.TabIndex = 0;
            // 
            // infoTab
            // 
            infoTab.Controls.Add(richTextBox2);
            infoTab.Location = new Point(4, 24);
            infoTab.Name = "infoTab";
            infoTab.Padding = new Padding(3);
            infoTab.Size = new Size(754, 364);
            infoTab.TabIndex = 0;
            infoTab.Text = "Info";
            infoTab.UseVisualStyleBackColor = true;
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(6, 6);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(742, 352);
            richTextBox2.TabIndex = 0;
            richTextBox2.Text = resources.GetString("richTextBox2.Text");
            // 
            // newPrefab
            // 
            newPrefab.Controls.Add(btnDeletePrefab);
            newPrefab.Controls.Add(textBox5);
            newPrefab.Controls.Add(label22);
            newPrefab.Controls.Add(btnUpdatePrefab);
            newPrefab.Controls.Add(btnCreateUpdate_PF);
            newPrefab.Controls.Add(tboxTags_PF);
            newPrefab.Controls.Add(tboxDescription_PF);
            newPrefab.Controls.Add(cboxHasPaidProductPlacement_PF);
            newPrefab.Controls.Add(cboxContainsSyntheticMedia_PF);
            newPrefab.Controls.Add(cboxSelfDeclaredMadeForKids_PF);
            newPrefab.Controls.Add(cboxCategory_PF);
            newPrefab.Controls.Add(label21);
            newPrefab.Controls.Add(tboxVideoTitle_PF);
            newPrefab.Controls.Add(label20);
            newPrefab.Controls.Add(cboxWhichPrefab_PF);
            newPrefab.Controls.Add(label19);
            newPrefab.Controls.Add(fboxLists_PF);
            newPrefab.Controls.Add(cboxPrivacyType_PF);
            newPrefab.Controls.Add(cboxMediaType_PF);
            newPrefab.Location = new Point(4, 24);
            newPrefab.Name = "newPrefab";
            newPrefab.Padding = new Padding(3);
            newPrefab.Size = new Size(754, 364);
            newPrefab.TabIndex = 1;
            newPrefab.Text = "Prefab Editor";
            newPrefab.UseVisualStyleBackColor = true;
            // 
            // btnDeletePrefab
            // 
            btnDeletePrefab.Location = new Point(573, 293);
            btnDeletePrefab.Name = "btnDeletePrefab";
            btnDeletePrefab.Size = new Size(56, 65);
            btnDeletePrefab.TabIndex = 36;
            btnDeletePrefab.Text = "Erase Prefab";
            btnDeletePrefab.UseVisualStyleBackColor = true;
            btnDeletePrefab.Click += btnDeletePrefab_Click;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(384, 6);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(364, 23);
            textBox5.TabIndex = 35;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(301, 9);
            label22.Name = "label22";
            label22.Size = new Size(77, 15);
            label22.TabIndex = 34;
            label22.Text = "Prefab name:";
            // 
            // btnUpdatePrefab
            // 
            btnUpdatePrefab.Location = new Point(635, 328);
            btnUpdatePrefab.Name = "btnUpdatePrefab";
            btnUpdatePrefab.Size = new Size(113, 30);
            btnUpdatePrefab.TabIndex = 33;
            btnUpdatePrefab.Text = "Update Prefab";
            btnUpdatePrefab.UseVisualStyleBackColor = true;
            btnUpdatePrefab.Click += btnUpdatePrefab_Click;
            // 
            // btnCreateUpdate_PF
            // 
            btnCreateUpdate_PF.Location = new Point(635, 293);
            btnCreateUpdate_PF.Name = "btnCreateUpdate_PF";
            btnCreateUpdate_PF.Size = new Size(113, 30);
            btnCreateUpdate_PF.TabIndex = 32;
            btnCreateUpdate_PF.Text = "Save new Prefab";
            btnCreateUpdate_PF.UseVisualStyleBackColor = true;
            btnCreateUpdate_PF.Click += button4_Click;
            // 
            // tboxTags_PF
            // 
            tboxTags_PF.Location = new Point(6, 293);
            tboxTags_PF.Multiline = true;
            tboxTags_PF.Name = "tboxTags_PF";
            tboxTags_PF.PlaceholderText = "video tags, separate with comma, space is not a separator";
            tboxTags_PF.ScrollBars = ScrollBars.Vertical;
            tboxTags_PF.Size = new Size(561, 65);
            tboxTags_PF.TabIndex = 31;
            // 
            // tboxDescription_PF
            // 
            tboxDescription_PF.AcceptsReturn = true;
            tboxDescription_PF.Location = new Point(6, 189);
            tboxDescription_PF.Multiline = true;
            tboxDescription_PF.Name = "tboxDescription_PF";
            tboxDescription_PF.PlaceholderText = "explain what you've done";
            tboxDescription_PF.ScrollBars = ScrollBars.Vertical;
            tboxDescription_PF.Size = new Size(742, 98);
            tboxDescription_PF.TabIndex = 30;
            // 
            // cboxHasPaidProductPlacement_PF
            // 
            cboxHasPaidProductPlacement_PF.AutoSize = true;
            cboxHasPaidProductPlacement_PF.Location = new Point(6, 133);
            cboxHasPaidProductPlacement_PF.Name = "cboxHasPaidProductPlacement_PF";
            cboxHasPaidProductPlacement_PF.Size = new Size(167, 19);
            cboxHasPaidProductPlacement_PF.TabIndex = 29;
            cboxHasPaidProductPlacement_PF.Text = "Is product placed in video?";
            cboxHasPaidProductPlacement_PF.UseVisualStyleBackColor = true;
            // 
            // cboxContainsSyntheticMedia_PF
            // 
            cboxContainsSyntheticMedia_PF.AutoSize = true;
            cboxContainsSyntheticMedia_PF.Location = new Point(6, 113);
            cboxContainsSyntheticMedia_PF.Name = "cboxContainsSyntheticMedia_PF";
            cboxContainsSyntheticMedia_PF.Size = new Size(129, 19);
            cboxContainsSyntheticMedia_PF.TabIndex = 28;
            cboxContainsSyntheticMedia_PF.Text = "Is made by clanker?";
            cboxContainsSyntheticMedia_PF.UseVisualStyleBackColor = true;
            // 
            // cboxSelfDeclaredMadeForKids_PF
            // 
            cboxSelfDeclaredMadeForKids_PF.AutoSize = true;
            cboxSelfDeclaredMadeForKids_PF.Location = new Point(6, 93);
            cboxSelfDeclaredMadeForKids_PF.Name = "cboxSelfDeclaredMadeForKids_PF";
            cboxSelfDeclaredMadeForKids_PF.Size = new Size(81, 19);
            cboxSelfDeclaredMadeForKids_PF.TabIndex = 27;
            cboxSelfDeclaredMadeForKids_PF.Text = "Is for kids?";
            cboxSelfDeclaredMadeForKids_PF.UseVisualStyleBackColor = true;
            // 
            // cboxCategory_PF
            // 
            cboxCategory_PF.FormattingEnabled = true;
            cboxCategory_PF.Location = new Point(83, 64);
            cboxCategory_PF.Name = "cboxCategory_PF";
            cboxCategory_PF.Size = new Size(192, 23);
            cboxCategory_PF.TabIndex = 26;
            cboxCategory_PF.SelectedIndexChanged += cboxCategory_PF_SelectedIndexChanged;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(6, 67);
            label21.Name = "label21";
            label21.Size = new Size(71, 15);
            label21.TabIndex = 25;
            label21.Text = "Category id:";
            // 
            // tboxVideoTitle_PF
            // 
            tboxVideoTitle_PF.Location = new Point(6, 35);
            tboxVideoTitle_PF.Name = "tboxVideoTitle_PF";
            tboxVideoTitle_PF.PlaceholderText = "Video title";
            tboxVideoTitle_PF.Size = new Size(269, 23);
            tboxVideoTitle_PF.TabIndex = 24;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(408, 38);
            label20.Name = "label20";
            label20.Size = new Size(78, 15);
            label20.TabIndex = 23;
            label20.Text = "Select prefab:";
            // 
            // cboxWhichPrefab_PF
            // 
            cboxWhichPrefab_PF.FormattingEnabled = true;
            cboxWhichPrefab_PF.Location = new Point(492, 35);
            cboxWhichPrefab_PF.Name = "cboxWhichPrefab_PF";
            cboxWhichPrefab_PF.Size = new Size(256, 23);
            cboxWhichPrefab_PF.TabIndex = 22;
            cboxWhichPrefab_PF.SelectedIndexChanged += cboxWhichPrefab_PF_SelectedIndexChanged;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(281, 38);
            label19.Name = "label19";
            label19.Size = new Size(101, 15);
            label19.TabIndex = 21;
            label19.Text = "Add to playlists ▼";
            // 
            // fboxLists_PF
            // 
            fboxLists_PF.AutoScroll = true;
            fboxLists_PF.Location = new Point(281, 64);
            fboxLists_PF.Name = "fboxLists_PF";
            fboxLists_PF.Size = new Size(467, 117);
            fboxLists_PF.TabIndex = 20;
            // 
            // cboxPrivacyType_PF
            // 
            cboxPrivacyType_PF.FormattingEnabled = true;
            cboxPrivacyType_PF.Location = new Point(133, 6);
            cboxPrivacyType_PF.Name = "cboxPrivacyType_PF";
            cboxPrivacyType_PF.Size = new Size(162, 23);
            cboxPrivacyType_PF.TabIndex = 12;
            cboxPrivacyType_PF.Text = "Set video privacy";
            cboxPrivacyType_PF.SelectedIndexChanged += cboxPrivacyType_PF_SelectedIndexChanged;
            // 
            // cboxMediaType_PF
            // 
            cboxMediaType_PF.FormattingEnabled = true;
            cboxMediaType_PF.Location = new Point(6, 6);
            cboxMediaType_PF.Name = "cboxMediaType_PF";
            cboxMediaType_PF.Size = new Size(121, 23);
            cboxMediaType_PF.TabIndex = 11;
            cboxMediaType_PF.Text = "Select media type";
            cboxMediaType_PF.SelectedIndexChanged += cboxMediaType_PF_SelectedIndexChanged;
            // 
            // managePrefabTab
            // 
            managePrefabTab.Location = new Point(4, 24);
            managePrefabTab.Name = "managePrefabTab";
            managePrefabTab.Size = new Size(754, 364);
            managePrefabTab.TabIndex = 2;
            managePrefabTab.Text = "Manage Prefabs";
            managePrefabTab.UseVisualStyleBackColor = true;
            // 
            // entriesTab
            // 
            entriesTab.Controls.Add(btnDeleteRow);
            entriesTab.Controls.Add(btnUploadNow);
            entriesTab.Controls.Add(btnCopyLink);
            entriesTab.Controls.Add(btnNavigate);
            entriesTab.Controls.Add(btnRefreshData);
            entriesTab.Controls.Add(dataEntryDisplay);
            entriesTab.Location = new Point(4, 24);
            entriesTab.Name = "entriesTab";
            entriesTab.Size = new Size(768, 398);
            entriesTab.TabIndex = 3;
            entriesTab.Text = "All entries";
            entriesTab.UseVisualStyleBackColor = true;
            // 
            // btnDeleteRow
            // 
            btnDeleteRow.Location = new Point(542, 372);
            btnDeleteRow.Name = "btnDeleteRow";
            btnDeleteRow.Size = new Size(84, 23);
            btnDeleteRow.TabIndex = 5;
            btnDeleteRow.Text = "Delete Entry";
            btnDeleteRow.UseVisualStyleBackColor = true;
            btnDeleteRow.Click += btnDeleteRow_Click;
            // 
            // btnUploadNow
            // 
            btnUploadNow.Location = new Point(632, 372);
            btnUploadNow.Name = "btnUploadNow";
            btnUploadNow.Size = new Size(133, 23);
            btnUploadNow.TabIndex = 4;
            btnUploadNow.Text = "Upload immediately";
            btnUploadNow.UseVisualStyleBackColor = true;
            btnUploadNow.Click += btnUploadNow_Click;
            // 
            // btnCopyLink
            // 
            btnCopyLink.Location = new Point(174, 372);
            btnCopyLink.Name = "btnCopyLink";
            btnCopyLink.Size = new Size(75, 23);
            btnCopyLink.TabIndex = 3;
            btnCopyLink.Text = "Copy Link";
            btnCopyLink.UseVisualStyleBackColor = true;
            // 
            // btnNavigate
            // 
            btnNavigate.Location = new Point(93, 372);
            btnNavigate.Name = "btnNavigate";
            btnNavigate.Size = new Size(75, 23);
            btnNavigate.TabIndex = 2;
            btnNavigate.Text = "Open Link";
            btnNavigate.UseVisualStyleBackColor = true;
            // 
            // btnRefreshData
            // 
            btnRefreshData.Location = new Point(3, 372);
            btnRefreshData.Name = "btnRefreshData";
            btnRefreshData.Size = new Size(84, 23);
            btnRefreshData.TabIndex = 1;
            btnRefreshData.Text = "Refresh grid";
            btnRefreshData.UseVisualStyleBackColor = true;
            btnRefreshData.Click += btnRefreshData_Click;
            // 
            // dataEntryDisplay
            // 
            dataEntryDisplay.AllowUserToAddRows = false;
            dataEntryDisplay.AllowUserToDeleteRows = false;
            dataEntryDisplay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataEntryDisplay.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataEntryDisplay.Location = new Point(3, 3);
            dataEntryDisplay.MultiSelect = false;
            dataEntryDisplay.Name = "dataEntryDisplay";
            dataEntryDisplay.ReadOnly = true;
            dataEntryDisplay.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataEntryDisplay.Size = new Size(762, 345);
            dataEntryDisplay.TabIndex = 0;
            // 
            // tickerConf
            // 
            tickerConf.Controls.Add(groupBox2);
            tickerConf.Controls.Add(groupBox1);
            tickerConf.Controls.Add(label9);
            tickerConf.Controls.Add(tboxTickerLog);
            tickerConf.Location = new Point(4, 24);
            tickerConf.Name = "tickerConf";
            tickerConf.Size = new Size(768, 398);
            tickerConf.TabIndex = 4;
            tickerConf.Text = "Configure Ticker";
            tickerConf.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(tboxHowManyUploaded);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(tboxWhenLastScan);
            groupBox2.Controls.Add(tboxNextScanTime);
            groupBox2.Controls.Add(label7);
            groupBox2.Location = new Point(13, 14);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(458, 104);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Scan datas";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 77);
            label11.Name = "label11";
            label11.Size = new Size(199, 15);
            label11.TabIndex = 12;
            label11.Text = "How much content uploaded today:";
            // 
            // tboxHowManyUploaded
            // 
            tboxHowManyUploaded.Location = new Point(308, 74);
            tboxHowManyUploaded.Name = "tboxHowManyUploaded";
            tboxHowManyUploaded.ReadOnly = true;
            tboxHowManyUploaded.Size = new Size(144, 23);
            tboxHowManyUploaded.TabIndex = 11;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 19);
            label10.Name = "label10";
            label10.Size = new Size(94, 15);
            label10.TabIndex = 8;
            label10.Text = "Last scan was at:";
            // 
            // tboxWhenLastScan
            // 
            tboxWhenLastScan.Location = new Point(308, 16);
            tboxWhenLastScan.Name = "tboxWhenLastScan";
            tboxWhenLastScan.ReadOnly = true;
            tboxWhenLastScan.Size = new Size(144, 23);
            tboxWhenLastScan.TabIndex = 9;
            // 
            // tboxNextScanTime
            // 
            tboxNextScanTime.Location = new Point(308, 45);
            tboxNextScanTime.Name = "tboxNextScanTime";
            tboxNextScanTime.ReadOnly = true;
            tboxNextScanTime.Size = new Size(144, 23);
            tboxNextScanTime.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 48);
            label7.Name = "label7";
            label7.Size = new Size(181, 15);
            label7.TabIndex = 2;
            label7.Text = "Next upload cycle happening in: ";
            label7.Click += label7_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label16);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(tickTrayMinimizeSetting);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(checkBox3);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(tickerIntervalSetting);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(tickerOnOff);
            groupBox1.Location = new Point(13, 124);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(458, 271);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "Ticker settings";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(6, 174);
            label16.Name = "label16";
            label16.Size = new Size(150, 15);
            label16.TabIndex = 15;
            label16.Text = "Remind to edit end screens";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Enabled = false;
            checkBox1.Location = new Point(308, 173);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(72, 19);
            checkBox1.TabIndex = 16;
            checkBox1.Text = "UNREAD";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Enabled = false;
            button3.Location = new Point(106, 242);
            button3.Name = "button3";
            button3.Size = new Size(94, 23);
            button3.TabIndex = 14;
            button3.Text = "Load settings";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Enabled = false;
            button2.Location = new Point(6, 242);
            button2.Name = "button2";
            button2.Size = new Size(94, 23);
            button2.TabIndex = 13;
            button2.Text = "Save settings";
            button2.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(6, 150);
            label14.Name = "label14";
            label14.Size = new Size(168, 15);
            label14.TabIndex = 11;
            label14.Text = "Minimize hides window in tray";
            // 
            // tickTrayMinimizeSetting
            // 
            tickTrayMinimizeSetting.AutoSize = true;
            tickTrayMinimizeSetting.Location = new Point(308, 149);
            tickTrayMinimizeSetting.Name = "tickTrayMinimizeSetting";
            tickTrayMinimizeSetting.Size = new Size(135, 19);
            tickTrayMinimizeSetting.TabIndex = 12;
            tickTrayMinimizeSetting.Text = "Minimizes to taskbar";
            tickTrayMinimizeSetting.UseVisualStyleBackColor = true;
            tickTrayMinimizeSetting.CheckedChanged += tickTrayMinimizeSetting_CheckedChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 125);
            label13.Name = "label13";
            label13.Size = new Size(226, 15);
            label13.TabIndex = 9;
            label13.Text = "Push uploads beyond 3 count to next day";
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Enabled = false;
            checkBox3.Location = new Point(308, 124);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(72, 19);
            checkBox3.TabIndex = 10;
            checkBox3.Text = "UNREAD";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new Point(308, 95);
            button1.Name = "button1";
            button1.Size = new Size(144, 23);
            button1.TabIndex = 8;
            button1.Text = "Configure Notifications";
            button1.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(6, 71);
            label12.Name = "label12";
            label12.Size = new Size(172, 15);
            label12.TabIndex = 6;
            label12.Text = "Enable/Disable OS notifications";
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Enabled = false;
            checkBox2.Location = new Point(308, 70);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(72, 19);
            checkBox2.TabIndex = 7;
            checkBox2.Text = "UNREAD";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 19);
            label6.Name = "label6";
            label6.Size = new Size(302, 15);
            label6.TabIndex = 0;
            label6.Text = "Check for content to be uploaded after every X minutes:";
            label6.Click += label6_Click;
            // 
            // tickerIntervalSetting
            // 
            tickerIntervalSetting.Location = new Point(308, 17);
            tickerIntervalSetting.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            tickerIntervalSetting.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            tickerIntervalSetting.Name = "tickerIntervalSetting";
            tickerIntervalSetting.Size = new Size(120, 23);
            tickerIntervalSetting.TabIndex = 5;
            tickerIntervalSetting.Value = new decimal(new int[] { 5, 0, 0, 0 });
            tickerIntervalSetting.ValueChanged += tickerIntervalSetting_ValueChanged_1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 46);
            label8.Name = "label8";
            label8.Size = new Size(117, 15);
            label8.TabIndex = 3;
            label8.Text = "Enable/Disable ticker";
            // 
            // tickerOnOff
            // 
            tickerOnOff.AutoSize = true;
            tickerOnOff.Location = new Point(308, 45);
            tickerOnOff.Name = "tickerOnOff";
            tickerOnOff.Size = new Size(72, 19);
            tickerOnOff.TabIndex = 4;
            tickerOnOff.Text = "UNREAD";
            tickerOnOff.UseVisualStyleBackColor = true;
            tickerOnOff.CheckedChanged += tickerOnOff_CheckedChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(477, 14);
            label9.Name = "label9";
            label9.Size = new Size(94, 15);
            label9.TabIndex = 7;
            label9.Text = "Ticker action log";
            // 
            // tboxTickerLog
            // 
            tboxTickerLog.Location = new Point(477, 36);
            tboxTickerLog.Multiline = true;
            tboxTickerLog.Name = "tboxTickerLog";
            tboxTickerLog.ReadOnly = true;
            tboxTickerLog.ScrollBars = ScrollBars.Vertical;
            tboxTickerLog.Size = new Size(288, 359);
            tboxTickerLog.TabIndex = 6;
            // 
            // aboutTab
            // 
            aboutTab.Controls.Add(richTextBox1);
            aboutTab.Location = new Point(4, 24);
            aboutTab.Name = "aboutTab";
            aboutTab.Size = new Size(768, 398);
            aboutTab.TabIndex = 5;
            aboutTab.Text = "About";
            aboutTab.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(3, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(762, 392);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = resources.GetString("richTextBox1.Text");
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // uploadTicker
            // 
            uploadTicker.Tick += uploadTicker_Tick;
            // 
            // trayIcon
            // 
            trayIcon.Icon = (Icon)resources.GetObject("trayIcon.Icon");
            trayIcon.Text = "YTA";
            trayIcon.Visible = true;
            trayIcon.DoubleClick += trayIcon_DoubleClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "YTA - Youtube Transfer Assistant";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Resize += Form1_Resize;
            tabControl1.ResumeLayout(false);
            loginTab.ResumeLayout(false);
            loginTab.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            loggedInInfo.ResumeLayout(false);
            loggedInInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)avatarBigImage).EndInit();
            calendarTab.ResumeLayout(false);
            calendarTab.PerformLayout();
            newEntryTab.ResumeLayout(false);
            newEntryTab.PerformLayout();
            prefabTab.ResumeLayout(false);
            tabControl2.ResumeLayout(false);
            infoTab.ResumeLayout(false);
            newPrefab.ResumeLayout(false);
            newPrefab.PerformLayout();
            entriesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataEntryDisplay).EndInit();
            tickerConf.ResumeLayout(false);
            tickerConf.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tickerIntervalSetting).EndInit();
            aboutTab.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage loginTab;
        private Button btnLogin;
        private TabPage calendarTab;
        private TabPage newEntryTab;
        private TabPage entriesTab;
        private Label labelChannelName;
        private Label labelIsLoggedIn;
        private Button btnLogOut;
        private FlowLayoutPanel scheduledEntries;
        private Label label1;
        private MonthCalendar scheduleCalendar;
        private Button btnRefreshCalendar;
        private Button btnImageBrowser;
        private TextBox tboxImagepath;
        private Button btnFileBrowser;
        private TextBox tboxVideopath;
        private ComboBox cboxMediaType;
        private TextBox tboxTags;
        private TextBox tboxDescription;
        private TextBox tboxTitle;
        private Label label2;
        private ComboBox cboxPrivacy;
        private Button btnAddEntry;
        private DateTimePicker dateTimePicker1;
        private CheckBox tickProduct;
        private CheckBox tickRobot;
        private CheckBox tickChild;
        private Button btnNavigate;
        private Button btnRefreshData;
        private DataGridView dataEntryDisplay;
        private Button btnCopyLink;
        private GroupBox loggedInInfo;
        private Label channelTitleBig;
        private Label videoCount;
        private Label label4;
        private Label subCount;
        private Label label3;
        private PictureBox avatarBigImage;
        private Label label5;
        private Label channelViewCount;
        private Button btnUploadNow;
        private TabPage tickerConf;
        private Label label6;
        private Label label7;
        private Label label8;
        private NumericUpDown tickerIntervalSetting;
        private CheckBox tickerOnOff;
        private Label label10;
        private Label label9;
        private TextBox tboxTickerLog;
        private GroupBox groupBox1;
        private TextBox tboxNextScanTime;
        private TextBox tboxWhenLastScan;
        private GroupBox groupBox2;
        private Label label11;
        private TextBox tboxHowManyUploaded;
        private Button button1;
        private Label label12;
        private CheckBox checkBox2;
        private Label label13;
        private CheckBox checkBox3;
        private Label label14;
        private CheckBox tickTrayMinimizeSetting;
        private System.Windows.Forms.Timer uploadTicker;
        private Button button3;
        private Button button2;
        private NotifyIcon trayIcon;
        private Button btnDeleteRow;
        private TabPage aboutTab;
        private RichTextBox richTextBox1;
        private GroupBox groupBox3;
        private TextBox textBox1;
        private ComboBox cboxCategory;
        private Label label15;
        private LinkLabel linkLabel1;
        private FlowLayoutPanel fboxPlaylists;
        private Label label17;
        private Label label18;
        private ComboBox cboxWhichPrefab;
        private TabPage prefabTab;
        private TabControl tabControl2;
        private TabPage infoTab;
        private TabPage newPrefab;
        private TabPage managePrefabTab;
        private RichTextBox richTextBox2;
        private Label label16;
        private CheckBox checkBox1;
        private ComboBox cboxPrivacyType_PF;
        private ComboBox cboxMediaType_PF;
        private Button btnUpdatePrefab;
        private Button btnCreateUpdate_PF;
        private TextBox tboxTags_PF;
        private TextBox tboxDescription_PF;
        private CheckBox cboxHasPaidProductPlacement_PF;
        private CheckBox cboxContainsSyntheticMedia_PF;
        private CheckBox cboxSelfDeclaredMadeForKids_PF;
        private ComboBox cboxCategory_PF;
        private Label label21;
        private TextBox tboxVideoTitle_PF;
        private Label label20;
        private ComboBox cboxWhichPrefab_PF;
        private Label label19;
        private FlowLayoutPanel fboxLists_PF;
        private TextBox textBox5;
        private Label label22;
        private Button btnDeletePrefab;
    }
}
