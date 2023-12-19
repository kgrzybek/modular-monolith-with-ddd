CREATE TABLE [usersmi].[Roles] 
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Name] NVARCHAR(256) NULL,
	[NormalizedName] NVARCHAR(256) NULL,
	[ConcurrencyStamp] NVARCHAR(max) NULL,    
	CONSTRAINT [PK_Role_Id] PRIMARY KEY ([Id]),
	CONSTRAINT [UQ_Role_Name] UNIQUE([Name]),
	CONSTRAINT [UQ_Role_NormalizedName] UNIQUE([NormalizedName])
)
GO