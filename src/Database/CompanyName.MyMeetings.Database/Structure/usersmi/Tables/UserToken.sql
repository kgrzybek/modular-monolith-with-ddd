CREATE TABLE [usersmi].[UserTokens] 
(
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [LoginProvider] NVARCHAR(450) NOT NULL,
    [Name] NVARCHAR(450) NOT NULL,
    [Value] NVARCHAR(max) NULL,
    CONSTRAINT [PK_UserToken_UserId_LoginProvider_Name] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_UserToken_UserId_User_Id] FOREIGN KEY ([UserId]) REFERENCES [usersmi].[Users] ([Id]) ON DELETE CASCADE
)
GO