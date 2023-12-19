CREATE TABLE [usersmi].[Permissions]
(
	[Code] VARCHAR(100) NOT NULL, 
	[Name] VARCHAR(100) NOT NULL, 
	[Description] VARCHAR(255) NULL,
	CONSTRAINT [PK_Permission_Code] PRIMARY KEY ([Code])
)
GO