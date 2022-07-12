CREATE TABLE [users].[Permissions]
(
	[Code] VARCHAR(50) NOT NULL,
	[Name] VARCHAR(100) NOT NULL,
	[Description] [varchar](255) NULL,
	CONSTRAINT [PK_users_Permissions_Code] PRIMARY KEY ([Code] ASC)
)