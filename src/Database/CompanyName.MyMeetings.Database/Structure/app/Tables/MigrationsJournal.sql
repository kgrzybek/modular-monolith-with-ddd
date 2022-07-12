CREATE TABLE [app].[MigrationsJournal](
	[Id] [int] IDENTITY(1, 1) NOT NULL,
	[ScriptName] NVARCHAR(255) NOT NULL,
	[Applied] DATETIME NOT NULL,
 CONSTRAINT [PK_app_MigrationsJournal_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
))