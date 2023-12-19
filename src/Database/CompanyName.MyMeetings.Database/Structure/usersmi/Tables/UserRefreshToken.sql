CREATE TABLE [usersmi].[UserRefreshTokens] 
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[Token] NVARCHAR(max) NOT NULL,
	[JwtId] NVARCHAR(max) NOT NULL,
	[IsRevoked] BIT NOT NULL,
	[AddedDate] DATETIME2 NOT NULL,
	[ExpiryDate] DATETIME2 NOT NULL,
	CONSTRAINT [PK_UserRefreshToken_Id] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_UserRefreshToken_UserId_User_Id] FOREIGN KEY ([UserId]) REFERENCES [usersmi].[Users] ([Id]) ON DELETE CASCADE
)
GO