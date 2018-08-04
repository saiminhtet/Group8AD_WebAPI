---------------------------------------------------------------------------
SA4106 Application Development Project
---------------------------------------------------------------------------
Prepared For:

Derek Kiong (Dr)	Principal Lecturer & Consultant
			Software Engineering & Design Practice
			isskbk@nus.edu.sg

------------------------------------------------------------

Prepared By: NUS ISS GDipSA46 Project Group 08

Han Myint Tun		A0180555A ( hanmyinttun@u.nus.edu )
Jiang Aiguo		A0180524L ( aiguo.jiang@u.nus.edu )
Noel Noel Han		A0180529B ( noelnoelhan@u.nus.edu )
Sai Min Htet		A0180545E ( saiminhtet@u.nus.edu )
Tang Shenqi		A0114523U ( tangshenqi@u.nus.edu )
Teong Hanlong		A0055905A ( teong@u.nus.edu )
Toh Shu Hui Sandy	A0180548Y ( sandytoh@u.nus.edu )
Zachary Chua Feng Kwan	A0180556B ( zacharychua@u.nus.edu )



============================================================
Contents
------------------------------------------------------------
0.0 Version History
1.0 Introduction
2.0 System Requirements
3.0 Database Setup
4.0 Application Setup
5.0 Authentication
6.0 Use Cases



============================================================
0.0 Version History
------------------------------------------------------------
Version: 	1.0
Date: 		2018-Aug-03,Fri
Released By:	Toh Shu Hui Sandy
Comment:	Initial Release

Version: 	1.1
Date: 		2018-Aug-05,Sun
Released By:	Teong Hanlong
Comment:	Updated Layout and Content



============================================================
1.0 Introduction
------------------------------------------------------------
The Project comprises of:
(1) Microsoft SQL Server Database
(2) ASP.NET Web API (Web Service)
(3) ASP.NET Web Forms (Web Application)
(4) Xamarin.Forms Mobile App (Mobile Application)



============================================================
2.0 System Requirements
------------------------------------------------------------
Supported Operating Systems: 64-bit edition

Windows 10 (version 1507 or higher): Home, Professional, Education, Enterprise
Windows 8.1 (with update 2919355): Core, Professional, Enterprise
Windows 7 SP1: Home Premium, Professional, Enterprise, Ultimate

---------------------------------------------

Hardware:

CPU:		 2.5 GHz minimum, quad-core or better recommended
RAM: 		 8GB minimum, 32GB recommended
Hard disk space: Roughly 15GB to 30GB of available space 
		 depending on features already installed
Hard disk speed: Solid-state drive recommended for better performance
Video card:	 Should support at least full HD (1920 x 1080) display resolution
Display size:	 At least 15" for better user experience

---------------------------------------------

Software:

(1)  SQL Server Management Studio
(2)  SQL Server 2017
(3)  SQL Server Data Tools
(4)  Visual Studio 2017 Enterprise Edition
     (i)    ASP.NET and Web Development Workload
     (ii)   Mobile Development with .NET Workload
(5)  Google Chrome Web Browser
(6)  Java SE Development Kit 8 64-bit
(7)  Android SDK Tools 26.1.1
(8)  Android SDK Platform-Tools 27.0.1
(9)  Android SDK Build-Tools 27.0.3
(10) Android Emulator 27.1.12
(11) Intel x86 Emulator Accelerator (HAXM installer) 6.2.1
(12) Android API 21 (Android 5.0 Lollipop)

---------------------------------------------

Supported Languages:

(1) English

---------------------------------------------

Additional Requirements:

- Administrator rights are required to run IIS server
- .NET Framework 4.6.1 is the target framework



============================================================
3.0 Database Setup
------------------------------------------------------------
If not previously restored or set up, please find the relevant 
database files in the App_Data folder of the respective projects.

-----------------------------------
Folders:
-----------------------------------
Web API: 	Group8AD_WebAPI\Group8AD_WebAPI\App_Data
Web App: 	Group8_AD_WebApp\Group8_AD_webapp\App_Data
Mobile App: 	ADProjGroup08\ADProjGroup08\MobileApp\MobileApp\App_Data

-----------------------------------
Files:
-----------------------------------
Physical File Name: 	Group8ASPNETDB.BAK
Logical	Database Name: 	aspnetdb
- This is the authentication database used for .NET Role-based security 
  which is implemented in the Web Application.

Physical File Name: 	SA46Team08ADProjectOAuthDB.BAK
Logical	Database Name: 	SA46Team08ADProjectOAuthDB
- This is the authentication database used for Microsoft OAuth 2.0 
  Token-based Authentication which is implemented in the Web API.

Physical File Name: 	SA46Team08ADProjectExtended.BAK
Logical	Database Name: 	SA46Team08ADProjectExtended
- This is the test data provided for testing Web and Mobile 
  Applications.

-----------------------------------
Restoration Procedure:
-----------------------------------
1. Launch SQL Server Management Studio
2. If database with conflicting name exists, please remove or detach
3. Select [Restore database...] option from context menu
4. In the new window that appears, browse for "Device" by clicking on [...] button
5. In the new window that appears, click on [Add] button
6. In the new window that appears, browse for desired BAK file and click [OK] button
7. Click [OK] button then [OK] button to proceed with restoration
8. Repeat above steps until all required databases are restored



============================================================
4.0 Application Setup
------------------------------------------------------------
1. Download entire workspace to a convenient location
2. Restore databases according to details in the above section
3. Open Visual Studio solution file for the Web API with Administrator rights
	Physical Name:  Group8AD_WebAPI.sln
	Folder: 	Group8AD_WebAPI
	
	3.1 Select [Publish...] from the context menu
	3.2 Configure "Target Location" via [Configure...] link
	3.3 Select "File System" from dropdown list
	3.4 Browse for "Target Location"
	3.5 Select [Local IIS] submenu
	3.6 Under "Default Web Site", create new web application named "Group8ADProjectApi"
	3.7 Click [OK] button then [Save] button then [Publish] button to publish Web API project

4. Open Visual Studio solution file for the Web App with Administrator rights
	Physical Name:  Group8_AD_webapp.sln
	Folder: 	Group8_AD_WebApp
	
	4.1 Perform steps similar to [3.1] to [3.7] to publish the Web App project, except
	    for step [3.6], name as "Group8ADProject".
	4.2 Launch Google Chrome with URL: http://localhost/Group8ADProject/Login.aspx

5. Open Visual Studio solution file for the Mobile App with Administrator rights
	Physical Name:  ADProjGroup08.sln
	Folder: 	ADProjGroup08

	5.1 Check that settings are correct:
	    5.1.1 Access "Options" from [Tools] > [Options] from the main menu
	    5.1.2 Scroll down and select [Xamarin] submenu
	    5.1.3 Check that "Java Development Kit Location" and "Android SDK Location" are set
	    5.1.4 Access "Android SDK Manager" from [Tools] > [Android] > [Android SDK Manager]
	    5.1.5 Check that Android Platform API and Tools are correct
	    5.1.6 Access "Android Device Manager" from [Tools] > [Android] > [Android Device Manager]
	    5.1.7 If no 7" fullHD device exists, please add a new device
		5.1.7.1 Select [Tablet M-DPI 7"] as "Base Device"
		5.1.7.2 Sekect [Lollipop 5.0 - API 21] as "OS"
		5.1.7.3 Ensure that "hw.lcd.density" is set to 160
		5.1.7.3 Ensure that "hw.lcd.height" is set to 1920
		5.1.7.3 Ensure that "hw.lcd.width" is set to 1080

	5.2 Launch "Command Prompt" from start menu
	5.3 Run "ipconfig" and obtain the IPv4 Address
	5.4 Under "Solution Explorer" panel, expand "Services" folder
	5.5 Open "UtilityService.cs"
	5.6 Under the "// CONSTANTS" commented block, modify the IP Address 
	    of the IIS server to your IP Address obtained in step [5.3]

	5.7 Select "MobileApp.Android" from the dropdown menu for "Startup Projects"
	5.8 Press [F5] to start debugging
	5.9 Android Emulator should launch

	Note: 
	- First launch of Android Emulator may take some time, please be patient
	- If the Android Virtual Device has just been newly created, setup may take some time as well
	- After Android Virtual Device has been loaded in Android Emulator, 
	  the mobile app will be installed
	- If it does not launch, please go back to Visual Studio 2017 and press [F5] again



============================================================
5.0 Authentication
------------------------------------------------------------
Microsoft OAuth 2.0 Token-based Authentication is implemented for bearer token type access to the Web API endpoints. This will affect the Mobile Application.

    Token-based authentication was chosen for use across heterogeneous 
    clients (e.g. browsers, native mobile app) to offer controlled access over a stipulated duration.

.NET Role-based security has been implemented for controlled access to Web Application.

---------------------------------------------

Login Credentials have been created as follows:

Role				   Employee ID	   Password     Access
-------------------------------	   ------------    ---------    ---------------
Department Employee (Employee)	   24		   1qazXSW@	Web App
Department Representative (Rep)	   23		   1qazXSW@	Web App, Mobile App
Department Delegate (Delegate)	   15		   1qazXSW@	Web App, Mobile App
Department Head (Head)	  	   6		   1qazXSW@	Web App, Mobile App

Store Clerk (Clerk)		   101		   1qazXSW@	Web App, Mobile App
Store Supervisor (Supervisor)	   105		   1qazXSW@	Web App
Store Manager (Manager)		   104		   1qazXSW@	Web App

---------------------------------------------

We have tested the email functions with both NUS and Gmail accounts.
But for the sake of workspace submission, we have created a Gmail account and 
set the email addresses for all employees to:

Account: 	logicuni714@gmail.com
Password:	logicuniversity

Emails are sent / received for the following use cases:
- Make Stationery Request
- Approve Stationery Request
- Reject Stationery Request
- Fulfil Stationery Request
- Accept Disbursement Items
- Make Adjustment Request
- Approve Adjustment Request
- Reject Adjustment Request
- Assign Delegate
- Assign Rep
- Set Collection Point



============================================================
6.0 Use Cases
------------------------------------------------------------
- Log In (All)
- Log Out (All)
- View Notification (All)
- Make / View / Update / Cancel Stationery Request (Employee, Rep)
- View / Approve / Reject Stationery Request (Head, Delegate)
- Fulfil Stationery Request (Clerk)
- Accept Disbursed Items (Rep)
- Assign / Relinquish Delegate (Head)
- Assign Rep (Head, Delegate)
- Set Collection Point (Rep)
- Receive Goods from Supplier (Clerk)
- Reorder Items with Low Stock Balance (Clerk)
- Print Inventory List (Clerk)
- Issue Stock Adjustment Request (Rep, Clerk)(Indirectly)
- Approve Stock Adjustment Request (Supervisor, Manager)
- Generate Dynamic Reports (Clerk, Supervisor, Manager)
- Update Supplier / Price Details (Clerk, Supervisor, Manager)
- Update Reorder Level / Quantity (Clerk, Supervisor, Manager)

Please refer to the User Manuals provided:

Physical Name: UserManual(Clerk,Supervisor,Manager).pdf
Physical Name: UserManual(Employee,Head,Rep).pdf

Location 1: ADProjGroup08\MobileApp\MobileApp
Location 2: Group8AD_WebAPI
Location 3: Group8_AD_WebApp



============================================================
Thank you for your patience. End of ReadMe.
============================================================