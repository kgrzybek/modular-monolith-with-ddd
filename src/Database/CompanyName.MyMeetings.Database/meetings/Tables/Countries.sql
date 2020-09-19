CREATE TABLE [meetings].[Countries]
(
	[Code] CHAR(2) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL
	CONSTRAINT [PK_meetings_Countries_Code] PRIMARY KEY ([Code] ASC)
)
GO