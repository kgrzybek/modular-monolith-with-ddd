CREATE TABLE [usersmi].[UserClaims] 
(
    [Id] INT NOT NULL IDENTITY,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [ClaimType] NVARCHAR(max) NULL,
    [ClaimValue] NVARCHAR(max) NULL,
    CONSTRAINT [PK_UserClaim_Id] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserClaim_UserId_User_Id] FOREIGN KEY ([UserId]) REFERENCES [usersmi].[Users] ([Id]) ON DELETE CASCADE
)
GO