CREATE TABLE [usersmi].[Users] 
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Name] NVARCHAR(255) NULL,
	[FirstName] NVARCHAR(100) NULL,
	[LastName] NVARCHAR(100) NULL,
	[UserName] NVARCHAR(256) NULL,
	[NormalizedUserName] NVARCHAR(256) NULL,
	[Email] NVARCHAR(256) NULL,
	[NormalizedEmail] NVARCHAR(256) NULL,
	[EmailConfirmed] BIT NOT NULL,
	[PasswordHash] NVARCHAR(max) NULL,
	[SecurityStamp] NVARCHAR(max) NULL,
	[ConcurrencyStamp] NVARCHAR(max) NULL,
	[PhoneNumber] NVARCHAR(max) NULL,
	[PhoneNumberConfirmed] BIT NOT NULL,
	[TwoFactorEnabled] BIT NOT NULL,
	[LockoutEnd] DATETIMEOFFSET NULL,
	[LockoutEnabled] BIT NOT NULL,
	[AccessFailedCount] INT NOT NULL,
	CONSTRAINT [PK_User_Id] PRIMARY KEY ([Id]),	
	CONSTRAINT [UQ_User_UserName] UNIQUE([UserName]),
	CONSTRAINT [UQ_User_NormalizedUserName] UNIQUE([NormalizedUserName])
)
GO