USE [master];
GO
IF DB_ID('Zrs_Projekat') IS NULL
BEGIN
    CREATE DATABASE [Zrs_Projekat];
END
GO

USE [Zrs_Projekat];
GO

/* Drop tables if exist */

DROP TABLE IF EXISTS [dbo].[BackupCodes];
GO
DROP TABLE IF EXISTS [dbo].[LoginAttempts];
GO
DROP TABLE IF EXISTS [dbo].[Users];
GO

USE [Zrs_Projekat];
GO



CREATE TABLE [dbo].[Users] (
    UserId BIGINT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    Salt NVARCHAR(100) NOT NULL,
    TwoFaSecretKey NVARCHAR(100),
    TwoFactorEnabled BIT NOT NULL DEFAULT 0
);
GO

CREATE TABLE [dbo].[BackupCodes] (
    BackupCodeId BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,
    Code NVARCHAR(100) NOT NULL,
    IsUsed BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Backup_Users FOREIGN KEY (UserId) REFERENCES [dbo].[Users](UserId) ON DELETE CASCADE
);
GO

CREATE TABLE [dbo].[LoginAttempts] (
    AttemptId BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,
    AttemptType SMALLINT NOT NULL, -- 0 for normal login, 1 for two-factor login
    AttemptTime DATETIME NOT NULL DEFAULT GETDATE(),
    Success BIT NOT NULL,
    CONSTRAINT FK_LoginAttempts_Users FOREIGN KEY (UserId) REFERENCES [dbo].[Users](UserId) ON DELETE CASCADE
);
GO
