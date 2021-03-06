USE [master]
GO
/****** Object:  Database [AcuCallTestDb]    Script Date: 8/22/2018 3:05:43 PM ******/
CREATE DATABASE [AcuCallTestDb]
GO
ALTER DATABASE [AcuCallTestDb] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AcuCallTestDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AcuCallTestDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AcuCallTestDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AcuCallTestDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AcuCallTestDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AcuCallTestDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [AcuCallTestDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [AcuCallTestDb] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [AcuCallTestDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AcuCallTestDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AcuCallTestDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AcuCallTestDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AcuCallTestDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AcuCallTestDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AcuCallTestDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AcuCallTestDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AcuCallTestDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [AcuCallTestDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AcuCallTestDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AcuCallTestDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AcuCallTestDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AcuCallTestDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AcuCallTestDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [AcuCallTestDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AcuCallTestDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AcuCallTestDb] SET  MULTI_USER 
GO
ALTER DATABASE [AcuCallTestDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AcuCallTestDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AcuCallTestDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AcuCallTestDb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
GO
ALTER DATABASE [AcuCallTestDb] SET ENABLE_BROKER;
GO
USE [AcuCallTestDb]
GO
/****** Object:  StoredProcedure [dbo].[GetUserSessionReport]    Script Date: 8/22/2018 3:05:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[GetUserSessionReport]
                        @Month int,
                        @Year int
                        AS
                        BEGIN
                        select cast(Login_DateTime as date) as [Date],
                                 max(coalesce(logins, 0) - coalesce(logouts, 0)) as MaxUsers
                          from (select l.Login_DateTime,
                                       (select count(*)
                                        from [UserSessions] l2
                                        where MONTH(l2.Login_DateTime) = @Month AND YEAR(l2.Login_DateTime) = @Year AND l2.Login_DateTime <= l.Login_DateTime
                                       ) as logins,
                                       (select count(*)
                                        from [UserSessions] l2
                                        where MONTH(l2.Logout_DateTime) = @Month AND YEAR(l2.Logout_DateTime) = @Year AND l2.Logout_DateTime <= l.Login_DateTime
                                       ) as logouts
                                from [UserSessions] l
                               ) l
                          group by cast(Login_DateTime as date);
                          END

GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/22/2018 3:05:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/22/2018 3:05:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserSessions]    Script Date: 8/22/2018 3:05:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSessions](
	[SessionId] [int] IDENTITY(1,1) NOT NULL,
	[Login_DateTime] [datetime2](7) NOT NULL,
	[Logout_DateTime] [datetime2](7) NULL,
	[Username] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserSessions] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IX_UserSessions_Login_DateTime]    Script Date: 8/22/2018 3:05:43 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserSessions_Login_DateTime] ON [dbo].[UserSessions]
(
	[Login_DateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserSessions_Logout_DateTime]    Script Date: 8/22/2018 3:05:43 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserSessions_Logout_DateTime] ON [dbo].[UserSessions]
(
	[Logout_DateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [AcuCallTestDb] SET  READ_WRITE 
GO
INSERT INTO [dbo].[Users]([FirstName], [LastName], [Username], [Password]) VALUES('Salman', 'Zahid', 'admin', 'admin');
GO
