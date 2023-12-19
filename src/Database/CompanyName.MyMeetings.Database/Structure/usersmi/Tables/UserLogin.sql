CREATE TABLE [usersmi].[UserLogins] 
(
    [LoginProvider] NVARCHAR(450) NOT NULL,
    [ProviderKey] NVARCHAR(450) NOT NULL,
    [ProviderDisplayName] NVARCHAR(max) NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UserLogin_LoginProvider_ProviderKey] PRIMARY KEY ([LoginProvider], [ProviderKey]),     
    CONSTRAINT [FK_UserLogin_UserId_User_Id] FOREIGN KEY ([UserId]) REFERENCES [usersmi].[Users] ([Id]) ON DELETE CASCADE
)
GO