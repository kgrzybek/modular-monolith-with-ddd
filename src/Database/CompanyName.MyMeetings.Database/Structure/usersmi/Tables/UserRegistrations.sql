﻿CREATE TABLE [usersmi].[UserRegistrations]
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [UserName] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR (255) NOT NULL,
    [Password] NVARCHAR(255) NOT NULL,
    [FirstName] NVARCHAR(50) NOT NULL,
    [LastName] NVARCHAR(50) NOT NULL,
    [Name] NVARCHAR (255) NOT NULL,
	[StatusCode] VARCHAR(50) NOT NULL,
	[RegisterDate] DATETIME NOT NULL,
	[ConfirmedDate] DATETIME NULL,
    CONSTRAINT [PK_UserRegistrations_Id] PRIMARY KEY ([Id] ASC)
)
GO