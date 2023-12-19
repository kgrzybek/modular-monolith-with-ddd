CREATE TABLE [usersmi].[RoleClaims] 
(
    [Id] int NOT NULL IDENTITY,
    [RoleId] UNIQUEIDENTIFIER NOT NULL,
    [ClaimType] NVARCHAR(max) NULL,
    [ClaimValue] NVARCHAR(max) NULL,
    CONSTRAINT [PK_RoleClaim_Id] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoleClaim_RoleId_Role_Id] FOREIGN KEY ([RoleId]) REFERENCES [usersmi].[Roles] ([Id]) ON DELETE CASCADE
)
GO