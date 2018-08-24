# Prerequisites
- Visual Studio 2017 version 15.7
- [.NET Core](https://www.microsoft.com/net/download)
- Microsoft SQL Server (Not required on local machine if database is hosted on a server)

# Steps To Run Project
1. Clone project
2. First of all make sure if you want to rename database name then update Database connection settings in appSettings.json -> ConnectionStrings -> AcuCallContext
3. You can create database, tables and stored procs using migration or script
4. If you want to run Db script then skip from step 5 to 8. Then navigate to Sql\DbScript.sql and execute it
5. Migration can be performed by opening Package Manager console and On top right selecting AcuCall.Infrastructure.Data from Default Project
6. Then run command `Update-Database`, It will create database if it does not exist else perform migrations
7. Once Migration is performed successfully, We need to enable Broker for Db notifications. So open your SQL Server instance
8. Connect to your server instance, and execute `ALTER DATABASE [DatabaseName] SET ENABLE_BROKER`
9. Executing migration or script will create a user **admin** with password **admin**
10. Now you are all ready to start application.