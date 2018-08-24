# Prerequisites
- Visual Studio 2017 version 15.7
- [.NET Core](https://www.microsoft.com/net/download)
- Microsoft SQL Server (Not required on local machine if database is hosted on a server)

# Steps To Run Project
1. Clone project.
2. First of all make sure if you want to rename database name then update Database connection settings in `appSettings.json -> ConnectionStrings -> AcuCallContext`.
3. You can create database, tables and stored procs using migration or script.
4. If you want to run Db script then skip from step 5 to 8. Then navigate to Sql\DbScript.sql and execute it.
5. Migration can be performed by opening Package Manager console and On top right selecting AcuCall.Infrastructure.Data from Default Project.
6. Then run command `Update-Database`, It will create database if it does not exist else perform migrations.
7. Once Migration is performed successfully, We need to enable Broker for Db notifications. So open your SQL Server instance.
8. Connect to your server instance, and execute `ALTER DATABASE [DatabaseName] SET ENABLE_BROKER`.
9. Executing migration or script will create a user **admin** with password **admin**.
10. Now you are all ready to start application.


# Deployment Of Project
The very first step is to install [.NET Core Runtime](https://www.microsoft.com/net/download/thank-you/dotnet-runtime-2.0.9-windows-hosting-bundle-installer) on your machine.

## Setup IIS Configuration

##### Windows Server Operating Systems
Enable the Web Server (IIS) server role and establish role services

1. Use the **Add Roles and Features** wizard from the **Manage** menu or the link in **Server Manager**. On the **Server Roles** step, check the box for **Web Server (IIS)**.
2. After the **Features** step, the **Role services** step loads for Web Server (IIS). Select the IIS role services desired or accept the default role services provided.
3. You can select different security levels from **Web Server > Security** if required.
4. Proceed through the **Confirmation** step to install the web server role and services. A server/IIS restart isn't required after installing the **Web Server (IIS)** role.

##### Windows Desktop Operating Systems
Enable the **IIS Management Console** and **World Wide Web Services**

1. Navigate to **Control Panel > Programs > Programs and Features > Turn Windows features on or off** (left side of the screen).
2. Open the **Internet Information Services** node. Open the **Web Management Tools** node.
3. Check the box for **IIS Management Console**.
4. Check the box for **World Wide Web Services**.
5. Accept the default features for **World Wide Web Services**.
6. You can select different security levels from **World Wide Web Services > Security** if required.

If the IIS installation requires a restart, restart the system.

## Now Create IIS Site
1. On the hosting system, create a folder to contain the app's published folders and files.
2. Within the new folder, create a logs folder to hold ASP.NET Core Module stdout logs when stdout logging is enabled. If logs folder already exist then we don't need to create it. This folder will hold error logs happening in website.
3. Open **IIS Manager** and then open the server's node in the **Connections** panel. Right-click the **Sites** folder. Select **Add Website** from the contextual menu.
![capture](https://user-images.githubusercontent.com/8514899/44599608-54b5f280-a7f0-11e8-8529-41bed62f4425.PNG)

4. Provide a **Site name** and set the **Physical path** to the app's deployment folder. Provide the **Binding** configuration by default port is 80 and create the website by selecting **OK**.
![capture1](https://user-images.githubusercontent.com/8514899/44599766-ca21c300-a7f0-11e8-9900-80993441bbb0.PNG)

5. Under the server's node, select **Application Pools**.
6. Right-click the site's app pool and select **Basic Settings** from the contextual menu.
7. In the **Edit Application Pool** window, set the **.NET CLR version** to **No Managed Code**. As ASP.NET Core runs in a separate process and manages the runtime. ASP.NET Core doesn't rely on loading the desktop CLR. Setting the **.NET CLR version** to **No Managed Code** is optional.
![capture2](https://user-images.githubusercontent.com/8514899/44599791-dad23900-a7f0-11e8-9e3a-26d39f0709bf.PNG)

## Deploy App
Deploy the app to the folder created on the hosting system by following below steps.

1. Right-click the project AcuCall.Web and select Publish.
2. When `Folder` is selected, specify a folder path to store the published assets. The default folder is `bin\Release\PublishOutput` but we need to set the path of **Physical Path** while creating new website in **IIS Manager**. Click the Publish button to finish.
![capture](https://user-images.githubusercontent.com/8514899/44598215-b162de80-a7eb-11e8-9a91-0add87e88060.PNG)


**NOW your application is served on localhost:[BindingPort]**

For more further information regarding deployment you can visit [Microsoft](https://docs.microsoft.com/en-gb/aspnet/core/host-and-deploy/iis/index?tabs=aspnetcore2x&view=aspnetcore-2.1) website. 


## Configure IIS To Access Website Using IP Address
1. Open **IIS Manager Console**, It can be found in **Administrative Tools -> Internet Information Services (IIS) Manager**.
2. In the **Connections** pane of IIS, expand the **Sites** and select the website **AcuCallAspNetCore** this is what i have named previously in this doc or the one you have named.
3. Click on **Bindings** link and you will see current bindings of that website.
![capture](https://user-images.githubusercontent.com/8514899/44600957-69948500-a7f4-11e8-904f-c0479c7a31ae.PNG)

4. Click on Add button.
5. On the **Add Site Binding** window, keep website **Type** as http. Select an **IP address** from the drop-down menu upon which you want to bind the website. Since other websites (along with their Host Header Values) are already bound on port 80, you won't be able to bind this new website on port 80 without Host Header Value (Host name). So, specify a port number (other than default port 80) on which you want to bind this new website. Keep **Host name** as blank, click **OK** and then **Close**. Once the binding is added in IIS Manager, the next step is allowing a port in Windows Firewall.
![capture2](https://user-images.githubusercontent.com/8514899/44601046-b4160180-a7f4-11e8-9c1f-47f2434b5052.PNG)
6. Go to **Administrative Tools -> Windows Defender Firewall with Advanced Security**.
7. At Windows Firewall window, click on **Inbound Rules**.
![capture](https://user-images.githubusercontent.com/8514899/44601514-26d3ac80-a7f6-11e8-8a90-8467314639b4.PNG)
8. Under **Actions** pane, click on **New Rule** and **New Inbound Rule Wizard** will be opened. On this window, select the **Port** radio button and click on **Next**.
![capture](https://user-images.githubusercontent.com/8514899/44601650-977ac900-a7f6-11e8-9c1f-405395b9fe6d.PNG)
9. On the next screen, select **TCP** and **Specific local ports** radio button. Specify a port number (upon which you set binding in IIS) in **Specific local ports** field and click **Next**.
![capture](https://user-images.githubusercontent.com/8514899/44601727-ddd02800-a7f6-11e8-91b8-b16dc25d9369.PNG)
10. On the next screen, select **Allow the connection** and click **Next**.
![capture](https://user-images.githubusercontent.com/8514899/44601877-4fa87180-a7f7-11e8-9dd2-61e76b736e5c.PNG)
11. Select the profiles for those we want to apply this rule and click **Next**.
![capture](https://user-images.githubusercontent.com/8514899/44601962-8a120e80-a7f7-11e8-8ef6-c9b15f25f4fb.PNG)
12. Very last step is to provide **Name** and **Description** for the newly created rule & click **Finish**.
![capture](https://user-images.githubusercontent.com/8514899/44602038-c2b1e800-a7f7-11e8-9a41-94bff90f44af.PNG)

**Now we are able to access your website using via IP address like http://VPS-IP-Address:81**


