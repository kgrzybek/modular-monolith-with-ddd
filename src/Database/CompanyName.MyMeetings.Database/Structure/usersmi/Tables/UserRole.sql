CREATE TABLE [usersmi].[UserRoles] 
(
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UserRole_UserId_RoleId] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRole_RoleId_Role_Id] FOREIGN KEY ([RoleId]) REFERENCES [usersmi].[Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRole_UserId_User_Id] FOREIGN KEY ([UserId]) REFERENCES [usersmi].[Users] ([Id]) ON DELETE CASCADE
)
GO