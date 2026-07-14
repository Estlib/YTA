# YTA

<img width="120" height="120" alt="image" src="https://github.com/user-attachments/assets/48848d03-b9d4-4c00-a21f-eb3177622893" />

#### Schedule content locally - upload only when needed.

---
## What is for?
Is for scheduling and upload automation locally, without the user needing to be present

## Why?
Sometimes I cannot be or do the things manually myself, so on youtube I tend to bulk upload, but that is not good for the algorithm, so this program automates the upload and scheduling process away from youtubes algorithm.

## But..  why?
This was fun to make.

---
# How to configure for use

Note: This instructional is made with V1.01 of program and may at times, be out of date:

### Step 1 - google side:
You need to follow this doc and configure the app to have OAUTH2 in google: https://support.google.com/googleapi/answer/6158849?hl=en
If youre using the prebuilt one, the prebuilt release docs show you the names you must use,
but for source version, you can define the names to be different in the source yourself, so for that reason, it is recommended you go with the source one.

### Step 2 - Users
Since youll most likely be using this only for your own accounts, there is no need to take it off Test Quotas, just add your necessary google account emails as test users for the project

### Step 3 - App side:
in app side. For the prebuilt, you need to rename the json file you get to "ycs.json" and place it in the YTAPIDATA folder.

<img width="178" height="75" alt="image" src="https://github.com/user-attachments/assets/1796f5bc-5488-453c-8a24-033be0ce4ade" />
<img width="558" height="98" alt="image" src="https://github.com/user-attachments/assets/3a4600a6-1689-4b67-88a9-9552deef1e2b" />

For the source version, you can do the same, or reconfigure the locations, names and formats. but the json is needed for api access.

This is where prebuilt instructions end. Report any problems under comments of this readme.

### Step 4 - Build
Build or publish your app for your usage. 

---
# How to use:

## 1. Interface
This is the main view and up top are all the tabs needed for the program to do its job:

<img width="814" height="498" alt="image" src="https://github.com/user-attachments/assets/804e7ca3-d3a1-4a07-ab1d-df56f4f9be71" />

The main view has a small Login/Logout section on the left with info about program and info about the user currently logged in.

## 2. Logging in
in order to successfully use the program, log in to your google account using the "Log In" button:

<img width="237" height="154" alt="image" src="https://github.com/user-attachments/assets/707b32c7-24fe-4d31-a328-46866e0f03b1" />

Browser window will open, asking you to log in, select the test user you registered earlier:

<img width="1054" height="323" alt="image" src="https://github.com/user-attachments/assets/15fca7db-73e9-4314-85bd-d1aae6c03f63" />

Google will notify you of the services registered in the Oauth2:

<img width="479" height="121" alt="image" src="https://github.com/user-attachments/assets/d07510f1-d367-47e9-94cc-155a6d205f1d" /> 
<img width="520" height="428" alt="image" src="https://github.com/user-attachments/assets/701d5361-9426-43d0-ad8c-a4b9ac251ccf" />

Accept

<img width="246" height="57" alt="image" src="https://github.com/user-attachments/assets/8e6bfe0b-262a-4b50-a587-18d6019b3f54" />

Page will notify of successful token:

<img width="416" height="40" alt="image" src="https://github.com/user-attachments/assets/c432ade7-2bc6-452d-ba2d-fa5b8db5ca86" />

and program will notify of a successful login:

<img width="192" height="165" alt="image" src="https://github.com/user-attachments/assets/522e5c72-5fd0-4d94-9a64-d708e7a78fd5" />

The main view will now also reflect the logged in user data:
<img width="815" height="498" alt="image" src="https://github.com/user-attachments/assets/3e258d8a-5c5f-42bc-8537-649692fb0313" />

## 3. Calendar tab
The calendar view shows you all entries planned for any given day. 
<img width="809" height="492" alt="image" src="https://github.com/user-attachments/assets/17a1a29a-8d41-4fdb-aef0-7bac53dacee3" />

By default, on navigating to the calendar tab, it refreshes the view and loads entries for current day. If no entries are found, a messagebox is shown. 
The messagebox is also shown when the user manually tries to display another day with no entries planned or uploaded:
<img width="818" height="498" alt="image" src="https://github.com/user-attachments/assets/c86ff6d8-c8df-47e5-b2e1-60de0a5c5f4b" />

When however entries are planned, it displayes them in a flowlayout using cards:
<img width="814" height="496" alt="image" src="https://github.com/user-attachments/assets/3a2a44a9-e75a-46d0-89bd-90465840ac28" />

## 4. Adding a new entry
Add Entry tab is where new entries will be defined for uploads.
Please note that while not pictured here, categories are programmatically obtained from the API, as some regions may have differing category selections.
For this reason, trying to add an entry prior to logging in WILL NOT WORK as the model validation will not allow you to post an entry without assigning it a category.
<img width="813" height="495" alt="image" src="https://github.com/user-attachments/assets/966c4e6c-7644-4f27-a0b2-b232a5e25777" />

### Fill the entry form with necessary info.

Here you select what type of entry this is. It is only for identification use later.

<img width="319" height="164" alt="image" src="https://github.com/user-attachments/assets/1211c772-147f-4b24-a734-422178f78c7a" />

Program will automodify this based on the video that you load, if it thinks it is incorrectly set.

Here you select what the privacy level of this video is and this is needed for youtube:

<img width="322" height="171" alt="image" src="https://github.com/user-attachments/assets/c7a4edeb-5b34-467c-bcd2-27110e9c9f0e" />

fill this with your video info, video file and thumbnail:

<img width="311" height="379" alt="image" src="https://github.com/user-attachments/assets/4b7e0f64-66ec-4890-95e8-f2f194baebe2" />

This datetime is for *when* you want the video to be uploaded from your pc. DO NOT confuse this datetime with the scheduling datetime you see in youtubes own scheduling window. This datetime is for local use by this app only. 

<img width="256" height="53" alt="image" src="https://github.com/user-attachments/assets/73c17abb-9b0a-4eca-9bb0-f751063038fe" />

Click add entry once done:

<img width="218" height="123" alt="image" src="https://github.com/user-attachments/assets/f35bd698-8b3a-4914-9a14-bf6866cb268b" />

For reference, this is the message you get when model validation fails:

<img width="424" height="253" alt="image" src="https://github.com/user-attachments/assets/2358fd27-4776-40e3-afdc-ddf376dcd56f" />

## 5. All entries tab

This tab displays all entries found in the database, and allows you to delete specific entries:
Tab refreshes everytime it is navigated to.

<img width="815" height="500" alt="image" src="https://github.com/user-attachments/assets/a8689d37-3490-49ed-9644-d5f736c17aea" />

To delete an entry, select a row and click on "Delete Entry":

<img width="816" height="495" alt="image" src="https://github.com/user-attachments/assets/943ef76c-5c25-46eb-94f8-c88266840fff" />

The "All Entries" tab is also used to sidestep the automatic ticking function. 
The "Upload Immediately" button performs an uploading procedure on any entry selected. Regardless of if it has been already uploaded or not.

<img width="189" height="80" alt="image" src="https://github.com/user-attachments/assets/1b06a774-8ec9-4df6-b66a-ea7d4470773c" />

"Refresh Grid" performs a manual refresh.
"Open Link" and "Copy Link" do not work yet

<img width="303" height="83" alt="image" src="https://github.com/user-attachments/assets/01a75005-9028-497f-b308-b8dac5f7234e" />

## 6. Configure Ticker tab

This is the tab that shows the ticker data and where you control the ticker itself

<img width="822" height="506" alt="image" src="https://github.com/user-attachments/assets/a6d3d85d-c462-45ef-aaa5-d660e1a12cd6" />

Scan datas contains info about the active work of the ticker. Specifically, it shows you when last scan happpened, when the next one is gonna be and how many uploads (of any kind) have been performed.

<img width="470" height="112" alt="image" src="https://github.com/user-attachments/assets/fb2c97c9-b322-4f23-9e22-20798d3cc22d" />

This is where the scheduled time comes into play. Namely, it is *not exact* no matter what you put in. The automation works as such that it performs checking cycles from the db between the interval of the scans and uploads everything that was supposed to be uploaded between last scan time and this scan time.

Or, put in a more visual term, here is a small, simplistic flowchart on how it works.

<img width="404" height="822" alt="image" src="https://github.com/user-attachments/assets/09a1ce5f-a303-4269-9253-b8b22e8a18e7" />

These are the real settings that the ticker runs on. 

<img width="467" height="284" alt="image" src="https://github.com/user-attachments/assets/be3e949d-e9c8-43fa-94b6-b820b797cac8" />

Smallest interval possible is 5 minutes, largest is 3600 minutes.

<img width="430" height="31" alt="image" src="https://github.com/user-attachments/assets/b15e382d-ad01-43ea-9b74-bd8d69f9fb3e" />

This enables or disables the ticker from running. The text next to the ticbox states what the current state of the option is.

<img width="385" height="23" alt="image" src="https://github.com/user-attachments/assets/05d5dc7b-c896-4fa7-a575-2ee13174d3af" />

When this setting is enabled, the minimize button hides the window in system tray instead of minimizing as usual:

Set to Minimize:

<img width="446" height="26" alt="image" src="https://github.com/user-attachments/assets/257c85cb-7059-45a6-97b9-113c29573b4f" />

Set to go to Tray:

<img width="419" height="24" alt="image" src="https://github.com/user-attachments/assets/6260f247-19ac-42ac-9152-bf4792310824" />

Message notifying of program in tray:

<img width="406" height="222" alt="image" src="https://github.com/user-attachments/assets/4ea6146f-008f-43f2-824b-f1f59d80914c" />

program in tray:

<img width="175" height="178" alt="image" src="https://github.com/user-attachments/assets/c4caae5a-3c1e-431d-870c-d873530deb86" />

These settings and options do not work yet:

<img width="465" height="76" alt="image" src="https://github.com/user-attachments/assets/b76efded-2dbb-44cd-a69a-b090955dd4f0" />
<img width="213" height="46" alt="image" src="https://github.com/user-attachments/assets/d6fe0d1f-1c67-4a4e-b6b7-6cc8a32a0f3a" />

The ticker action log records all of the actions by date time for the user to see. This does not evoke messageboxes or bubbles or anything. It is just a log.

<img width="307" height="403" alt="image" src="https://github.com/user-attachments/assets/7757d610-cf73-4fde-bd1d-f596cc113bd5" />

---

## Example Use Case

https://youtu.be/E54KBRhn51o
