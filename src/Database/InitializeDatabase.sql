
GO
PRINT N'Creating [administration]...';


GO
CREATE SCHEMA [administration]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating [meetings]...';


GO
CREATE SCHEMA [meetings]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating [payments]...';


GO
CREATE SCHEMA [payments]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating [users]...';


GO
CREATE SCHEMA [users]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating [administration].[InternalCommands]...';


GO
CREATE TABLE [administration].[InternalCommands] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [EnqueueDate]   DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    [Error] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_administration_InternalCommands_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [administration].[InboxMessages]...';


GO
CREATE TABLE [administration].[InboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_administration_InboxMessages_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [administration].[OutboxMessages]...';


GO
CREATE TABLE [administration].[OutboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_administration_OutboxMessages_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [administration].[MeetingGroupProposals]...';


GO
CREATE TABLE [administration].[MeetingGroupProposals] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [Name]                 NVARCHAR (255)   NOT NULL,
    [Description]          VARCHAR (200)    NULL,
    [LocationCity]         NVARCHAR (50)    NOT NULL,
    [LocationCountryCode]  NVARCHAR (3)     NOT NULL,
    [ProposalUserId]       UNIQUEIDENTIFIER NOT NULL,
    [ProposalDate]         DATETIME         NOT NULL,
    [StatusCode]           NVARCHAR (50)    NOT NULL,
    [DecisionDate]         DATETIME         NULL,
    [DecisionUserId]       UNIQUEIDENTIFIER NULL,
    [DecisionCode]         NVARCHAR (50)    NULL,
    [DecisionRejectReason] NVARCHAR (250)   NULL,
    CONSTRAINT [PK_administration_MeetingGroupProposals_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [administration].[Members]...';


GO
CREATE TABLE [administration].[Members] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Login]     NVARCHAR (100)   NOT NULL,
    [Email]     NVARCHAR (255)   NOT NULL,
    [FirstName] NVARCHAR (50)    NOT NULL,
    [LastName]  NVARCHAR (50)    NOT NULL,
    [Name]      NVARCHAR (255)   NOT NULL,
    CONSTRAINT [PK_administration_Members_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [meetings].[MeetingWaitlistMembers]...';


GO
CREATE TABLE [meetings].[MeetingWaitlistMembers] (
    [MeetingId]            UNIQUEIDENTIFIER NOT NULL,
    [MemberId]             UNIQUEIDENTIFIER NOT NULL,
    [SignUpDate]           DATETIME2 (7)    NOT NULL,
    [IsSignedOff]          BIT              NOT NULL,
    [SignOffDate]          DATETIME2 (7)    NULL,
    [IsMovedToAttendees]   BIT              NOT NULL,
    [MovedToAttendeesDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_meetings_MeetingWaitlistMembers_MeetingId_MemberId_SignUpDate] PRIMARY KEY CLUSTERED ([MeetingId] ASC, [MemberId] ASC, [SignUpDate] ASC)
);


GO
PRINT N'Creating [meetings].[MeetingNotAttendees]...';


GO
CREATE TABLE [meetings].[MeetingNotAttendees] (
    [MeetingId]          UNIQUEIDENTIFIER NOT NULL,
    [MemberId]           UNIQUEIDENTIFIER NOT NULL,
    [DecisionDate]       DATETIME2 (7)    NOT NULL,
    [DecisionChanged]    BIT              NOT NULL,
    [DecisionChangeDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_meetings_MeetingNotAttendees_Id] PRIMARY KEY CLUSTERED ([MeetingId] ASC, [MemberId] ASC, [DecisionDate] ASC)
);


GO
PRINT N'Creating [meetings].[MeetingAttendees]...';


GO
CREATE TABLE meetings.MeetingAttendees
(
	[MeetingId] UNIQUEIDENTIFIER NOT NULL,
	[AttendeeId] UNIQUEIDENTIFIER NOT NULL,
	[DecisionDate] DATETIME2 NOT NULL,
	[RoleCode] VARCHAR(50) NULL,   
    [GuestsNumber] INT NULL,
    [DecisionChanged] BIT NOT NULL,
    [DecisionChangeDate] DATETIME2 NULL,
	[IsRemoved] BIT NOT NULL,
	[RemovingMemberId] UNIQUEIDENTIFIER NULL,
	[RemovingReason] NVARCHAR(500) NULL,
	[RemovedDate] DATETIME2 NULL,
	[BecameNotAttendeeDate] DATETIME2 NULL,
	[FeeValue] DECIMAL(5, 0) NULL,
    [FeeCurrency] VARCHAR(3) NULL,
	[IsFeePaid] BIT NOT NULL,
	CONSTRAINT [PK_meetings_MeetingAttendees_Id] PRIMARY KEY ([MeetingId] ASC, [AttendeeId] ASC, [DecisionDate] ASC)
)
GO


GO
PRINT N'Creating [meetings].[Meetings]...';


GO
CREATE TABLE [meetings].[Meetings] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [MeetingGroupId]     UNIQUEIDENTIFIER NOT NULL,
    [CreatorId]          UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]         DATETIME2 (7)    NOT NULL,
    [Title]              NVARCHAR (200)   NOT NULL,
    [Description]        NVARCHAR (4000)  NOT NULL,
    [TermStartDate]      DATETIME         NOT NULL,
    [TermEndDate]        DATETIME         NOT NULL,
    [LocationName]       NVARCHAR (200)   NOT NULL,
    [LocationAddress]    NVARCHAR (200)   NOT NULL,
    [LocationPostalCode] NVARCHAR (200)   NULL,
    [LocationCity]       NVARCHAR (50)    NOT NULL,
    [AttendeesLimit]     INT              NULL,
    [GuestsLimit]        INT              NOT NULL,
    [RSVPTermStartDate]  DATETIME         NULL,
    [RSVPTermEndDate]    DATETIME         NULL,
    [EventFeeValue]      DECIMAL (5)      NULL,
    [EventFeeCurrency]   VARCHAR (3)      NULL,
    [ChangeDate]         DATETIME2 (7)    NULL,
    [ChangeMemberId]     UNIQUEIDENTIFIER NULL,
    [CancelDate]         DATETIME         NULL,
    [CancelMemberId]     UNIQUEIDENTIFIER NULL,
    [IsCanceled]         BIT              NOT NULL,
    CONSTRAINT [PK_meetings_Meetings_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [meetings].[MeetingGroupMembers]...';


GO
CREATE TABLE [meetings].[MeetingGroupMembers] (
    [MeetingGroupId] UNIQUEIDENTIFIER NOT NULL,
    [MemberId]       UNIQUEIDENTIFIER NOT NULL,
    [JoinedDate]     DATETIME2 (7)    NOT NULL,
    [RoleCode]       VARCHAR (50)     NOT NULL,
    [IsActive]       BIT              NOT NULL,
    [LeaveDate]      DATETIME         NULL,
    CONSTRAINT [PK_meetings_MeetingGroupMembers_MeetingGroupId_MemberId_JoinedDate] PRIMARY KEY CLUSTERED ([MeetingGroupId] ASC, [MemberId] ASC, [JoinedDate] ASC)
);


GO
PRINT N'Creating [meetings].[MeetingGroups]...';


GO
CREATE TABLE [meetings].[MeetingGroups] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [Name]                NVARCHAR (255)   NOT NULL,
    [Description]         VARCHAR (200)    NULL,
    [LocationCity]        NVARCHAR (50)    NOT NULL,
    [LocationCountryCode] NVARCHAR (3)     NOT NULL,
    [CreatorId]           UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]          DATETIME         NOT NULL,
    [PaymentDateTo]       DATE             NULL,
    CONSTRAINT [PK_meetings_MeetingGroups_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [meetings].[Members]...';


GO
CREATE TABLE [meetings].[Members] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Login]     NVARCHAR (100)   NOT NULL,
    [Email]     NVARCHAR (255)   NOT NULL,
    [FirstName] NVARCHAR (50)    NOT NULL,
    [LastName]  NVARCHAR (50)    NOT NULL,
    [Name]      NVARCHAR (255)   NOT NULL,
    CONSTRAINT [PK_meetings_Members_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [meetings].[MeetingGroupProposals]...';


GO
CREATE TABLE [meetings].[MeetingGroupProposals] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [Name]                NVARCHAR (255)   NOT NULL,
    [Description]         VARCHAR (200)    NULL,
    [LocationCity]        NVARCHAR (50)    NOT NULL,
    [LocationCountryCode] NVARCHAR (3)     NOT NULL,
    [ProposalUserId]      UNIQUEIDENTIFIER NOT NULL,
    [ProposalDate]        DATETIME         NOT NULL,
    [StatusCode]          NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_meetings_MeetingGroupProposals_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [meetings].[OutboxMessages]...';


GO
CREATE TABLE [meetings].[OutboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_meetings_OutboxMessages_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [meetings].[InternalCommands]...';


GO
CREATE TABLE [meetings].[InternalCommands] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [EnqueueDate]   DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    [Error] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_meetings_InternalCommands_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [meetings].[InboxMessages]...';


GO
CREATE TABLE [meetings].[InboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_meetings_InboxMessages_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

PRINT N'Creating [payments].[Payers]...';


GO
CREATE TABLE [payments].[Payers] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Login]     NVARCHAR (100)   NOT NULL,
    [Email]     NVARCHAR (255)   NOT NULL,
    [FirstName] NVARCHAR (50)    NOT NULL,
    [LastName]  NVARCHAR (50)    NOT NULL,
    [Name]      NVARCHAR (255)   NOT NULL,
    CONSTRAINT [PK_payments_Payers_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO


PRINT N'Creating [payments].[OutboxMessages]...';


GO
CREATE TABLE [payments].[OutboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_payments_OutboxMessages_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [payments].[InternalCommands]...';


GO
CREATE TABLE [payments].[InternalCommands] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [EnqueueDate]   DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    [Error] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_payments_InternalCommands_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [payments].[InboxMessages]...';


GO
CREATE TABLE [payments].[InboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_payments_InboxMessages_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [users].[InboxMessages]...';


GO
CREATE TABLE [users].[InboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_users_InboxMessages_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [users].[UserRoles]...';


GO
CREATE TABLE [users].[UserRoles] (
    [UserId]   UNIQUEIDENTIFIER NOT NULL,
    [RoleCode] NVARCHAR (50)    NULL
);


GO
PRINT N'Creating [users].[UserRegistrations]...';


GO
CREATE TABLE [users].[UserRegistrations] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [Login]         NVARCHAR (100)   NOT NULL,
    [Email]         NVARCHAR (255)   NOT NULL,
    [Password]      NVARCHAR (255)   NOT NULL,
    [FirstName]     NVARCHAR (50)    NOT NULL,
    [LastName]      NVARCHAR (50)    NOT NULL,
    [Name]          NVARCHAR (255)   NOT NULL,
    [StatusCode]    VARCHAR (50)     NOT NULL,
    [RegisterDate]  DATETIME         NOT NULL,
    [ConfirmedDate] DATETIME         NULL,
    CONSTRAINT [PK_users_UserRegistrations_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [users].[Users]...';


GO
CREATE TABLE [users].[Users] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Login]     NVARCHAR (100)   NOT NULL,
    [Email]     NVARCHAR (255)   NOT NULL,
    [Password]  NVARCHAR (255)   NOT NULL,
    [IsActive]  BIT              NOT NULL,
    [FirstName] NVARCHAR (50)    NOT NULL,
    [LastName]  NVARCHAR (50)    NOT NULL,
    [Name]      NVARCHAR (255)   NOT NULL,
    CONSTRAINT [PK_users_Users_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [users].[RolesToPermissions]...';


GO
CREATE TABLE [users].[RolesToPermissions] (
    [RoleCode]       VARCHAR (50) NOT NULL,
    [PermissionCode] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_RolesToPermissions_RoleCode_PermissionCode] PRIMARY KEY CLUSTERED ([RoleCode] ASC, [PermissionCode] ASC)
);


GO
PRINT N'Creating [users].[Permissions]...';


GO
CREATE TABLE [users].[Permissions] (
    [Code]        VARCHAR (50)  NOT NULL,
    [Name]        VARCHAR (100) NOT NULL,
    [Description] VARCHAR (255) NULL,
    CONSTRAINT [PK_users_Permissions_Code] PRIMARY KEY CLUSTERED ([Code] ASC)
);


GO
PRINT N'Creating [users].[InternalCommands]...';


GO
CREATE TABLE [users].[InternalCommands] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [EnqueueDate]   DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    [Error] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_users_InternalCommands_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [users].[OutboxMessages]...';


GO
CREATE TABLE [users].[OutboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_users_OutboxMessages_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [meetings].[v_MeetingGroups]...';


GO

CREATE VIEW [meetings].[v_MeetingGroups]
AS
SELECT
    [MeetingGroup].Id,
    [MeetingGroup].[Name],
    [MeetingGroup].[Description],
    [MeetingGroup].[LocationCountryCode],
    [MeetingGroup].[LocationCity]
FROM meetings.MeetingGroups AS [MeetingGroup]
GO
PRINT N'Creating [meetings].[v_Meetings]...';


GO

CREATE VIEW [meetings].[v_Meetings]
AS
SELECT
	Meeting.[Id],
    Meeting.[Title],
    Meeting.[Description],
    Meeting.LocationAddress,
    Meeting.LocationCity,
    Meeting.LocationPostalCode,
    Meeting.TermStartDate,
    Meeting.TermEndDate
FROM meetings.Meetings AS [Meeting]
GO
PRINT N'Creating [meetings].[v_Members]...';


GO
CREATE VIEW [meetings].[v_Members]
AS
SELECT
    [Member].Id,
    [Member].[Name],
    [Member].[Login],
    [Member].[Email]
FROM meetings.Members AS [Member]
GO
PRINT N'Creating [users].[v_UserRoles]...';


GO
CREATE VIEW [users].[v_UserRoles]
AS
SELECT
    [UserRole].[UserId],
    [UserRole].[RoleCode]
FROM [users].[UserRoles] AS [UserRole]
GO
PRINT N'Creating [users].[v_Users]...';


GO
CREATE VIEW [users].[v_Users]
AS
SELECT
    [User].[Id],
    [User].[IsActive],
    [User].[Login],
    [User].[Password],
    [User].[Email],
    [User].[Name]
FROM [users].[Users] AS [User]
GO
PRINT N'Creating [users].[v_UserPermissions]...';


GO
CREATE VIEW [users].[v_UserPermissions]
AS
SELECT 
	DISTINCT
	[UserRole].UserId,
	[RolesToPermission].PermissionCode
FROM [users].UserRoles AS [UserRole]
	INNER JOIN [users].RolesToPermissions AS [RolesToPermission]
		ON [UserRole].RoleCode = [RolesToPermission].RoleCode
GO
PRINT N'Update complete.';


GO

CREATE VIEW [users].[v_UserRegistrations]
AS
SELECT
    [UserRegistration].[Id],
    [UserRegistration].[Login],
    [UserRegistration].[Email],
    [UserRegistration].[FirstName],
    [UserRegistration].[LastName],
    [UserRegistration].[Name],
    [UserRegistration].[StatusCode]
FROM [users].[UserRegistrations] AS [UserRegistration]
GO

CREATE VIEW [administration].[v_Members]
AS
SELECT
    [Member].[Id],
    [Member].[Login],
    [Member].[Email],
    [Member].[FirstName],
    [Member].[LastName],
    [Member].[Name]
FROM [administration].[Members] AS [Member]
GO

CREATE VIEW [administration].[v_MeetingGroupProposals]
AS
SELECT
    [MeetingGroupProposal].[Id],
    [MeetingGroupProposal].[Name],
    [MeetingGroupProposal].[Description],
    [MeetingGroupProposal].[LocationCity],
    [MeetingGroupProposal].[LocationCountryCode],
    [MeetingGroupProposal].[ProposalUserId],
    [MeetingGroupProposal].[ProposalDate],
    [MeetingGroupProposal].[StatusCode],
    [MeetingGroupProposal].[DecisionDate],
    [MeetingGroupProposal].[DecisionUserId],
    [MeetingGroupProposal].[DecisionCode],
    [MeetingGroupProposal].[DecisionRejectReason]
FROM [administration].[MeetingGroupProposals] AS [MeetingGroupProposal]
GO

CREATE VIEW [meetings].[v_MeetingGroupProposals]
AS
SELECT
    [MeetingGroupProposal].[Id],
    [MeetingGroupProposal].[Name],
    [MeetingGroupProposal].[Description],
    [MeetingGroupProposal].[LocationCity],
    [MeetingGroupProposal].[LocationCountryCode],
    [MeetingGroupProposal].[ProposalUserId],
    [MeetingGroupProposal].[ProposalDate],
    [MeetingGroupProposal].[StatusCode]
FROM [meetings].[MeetingGroupProposals] AS [MeetingGroupProposal]
GO

-- Initialize some data


/* SQL Server 2012+*/

DECLARE @DBName sysname;
SET @DBName = (SELECT db_name());
DECLARE @SQL varchar(1000);
SET @SQL = 'ALTER DATABASE ['+@DBName+'] SET ALLOW_SNAPSHOT_ISOLATION ON; ALTER DATABASE ['+@DBName+'] SET READ_COMMITTED_SNAPSHOT ON;'; 
exec(@sql)

IF OBJECT_ID('payments.Streams', 'U') IS NULL
BEGIN
    CREATE TABLE payments.Streams(
        Id                  CHAR(42)                                NOT NULL,
        IdOriginal          NVARCHAR(1000)                          NOT NULL,
        IdInternal          INT                 IDENTITY(1,1)       NOT NULL,
        [Version]           INT                 DEFAULT(-1)         NOT NULL,
        Position            BIGINT              DEFAULT(-1)         NOT NULL,
        CONSTRAINT PK_Streams PRIMARY KEY CLUSTERED (IdInternal)
    );
END

IF NOT EXISTS(
    SELECT * 
    FROM sys.indexes
    WHERE name='IX_Streams_Id' AND object_id = OBJECT_ID('payments.Streams', 'U'))
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX IX_Streams_Id ON payments.Streams (Id);
END
 
IF object_id('payments.Messages', 'U') IS NULL
BEGIN
    CREATE TABLE payments.Messages(
        StreamIdInternal    INT                                     NOT NULL,
        StreamVersion       INT                                     NOT NULL,
        Position            BIGINT                 IDENTITY(0,1)    NOT NULL,
        Id                  UNIQUEIDENTIFIER                        NOT NULL,
        Created             DATETIME                                NOT NULL,
        [Type]              NVARCHAR(128)                           NOT NULL,
        JsonData            NVARCHAR(max)                           NOT NULL,
        JsonMetadata        NVARCHAR(max)                                   ,
        CONSTRAINT PK_Events PRIMARY KEY NONCLUSTERED (Position),
        CONSTRAINT FK_Events_Streams FOREIGN KEY (StreamIdInternal) REFERENCES payments.Streams(IdInternal)
    );
END

IF NOT EXISTS(
    SELECT * 
    FROM sys.indexes
    WHERE name='IX_Messages_Position' AND object_id = OBJECT_ID('payments.Messages'))
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX IX_Messages_Position ON payments.Messages (Position);
END

IF NOT EXISTS(
    SELECT * 
    FROM sys.indexes
    WHERE name='IX_Messages_StreamIdInternal_Id' AND object_id = OBJECT_ID('payments.Messages'))
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX IX_Messages_StreamIdInternal_Id ON payments.Messages (StreamIdInternal, Id);
END

IF NOT EXISTS(
    SELECT * 
    FROM sys.indexes
    WHERE name='IX_Messages_StreamIdInternal_Revision' AND object_id = OBJECT_ID('payments.Messages'))
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX IX_Messages_StreamIdInternal_Revision ON payments.Messages (StreamIdInternal, StreamVersion);
END

IF NOT EXISTS(
    SELECT * 
    FROM sys.indexes
    WHERE name='IX_Messages_StreamIdInternal_Created' AND object_id = OBJECT_ID('payments.Messages'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_Messages_StreamIdInternal_Created ON payments.Messages (StreamIdInternal, Created);
END

IF NOT EXISTS(
    SELECT * 
    FROM sys.table_types tt JOIN sys.schemas s ON tt.schema_id = s.schema_id
    WHERE s.name + '.' + tt.name='payments.NewStreamMessages')
BEGIN
    CREATE TYPE payments.NewStreamMessages AS TABLE (
        StreamVersion       INT IDENTITY(0,1)                       NOT NULL,
        Id                  UNIQUEIDENTIFIER                        NOT NULL,
        Created             DATETIME          DEFAULT(GETUTCDATE()) NOT NULL,
        [Type]              NVARCHAR(128)                           NOT NULL,
        JsonData            NVARCHAR(max)                           NULL,
        JsonMetadata        NVARCHAR(max)                           NULL
    );
END

BEGIN
    IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('payments.Streams') AND [name] = N'version' AND [minor_id] = 0)
    EXEC sys.sp_addextendedproperty   
    @name = N'version',
    @value = N'2',
    @level0type = N'SCHEMA', @level0name = 'payments',
    @level1type = N'TABLE',  @level1name = 'Streams';
END

CREATE TABLE payments.SubscriptionDetails
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Period] VARCHAR(50) NOT NULL,
    [Status] VARCHAR(50) NOT NULL,
    [CountryCode] VARCHAR(50) NOT NULL,
    [ExpirationDate] DATETIME NOT NULL,
    CONSTRAINT [PK_payments_SubscriptionDetails_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE payments.SubscriptionCheckpoints
(
    [Code] VARCHAR(50) NOT NULL,
    [Position] BIGINT NOT NULL
)

CREATE TABLE payments.PriceListItems
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [SubscriptionPeriodCode] VARCHAR(50) NOT NULL,
    [CategoryCode] VARCHAR(50) NOT NULL,
    [CountryCode] VARCHAR(50) NOT NULL,
    [MoneyValue] DECIMAL(18, 2) NOT NULL,
    [MoneyCurrency] VARCHAR(50) NOT NULL,
    [IsActive] BIT NOT NULL
)

CREATE TABLE payments.SubscriptionPayments
(
    [PaymentId] UNIQUEIDENTIFIER NOT NULL,
    [PayerId] UNIQUEIDENTIFIER NOT NULL,
    [Type] VARCHAR(50) NOT NULL,
    [Status] VARCHAR(50) NOT NULL,
    [Period] VARCHAR(50) NOT NULL,
    [Date] DATETIME NOT NULL,
    [SubscriptionId] UNIQUEIDENTIFIER NULL,
    [MoneyValue] DECIMAL(18, 2) NOT NULL,
    [MoneyCurrency] VARCHAR(50) NOT NULL
)

CREATE TABLE [meetings].[MemberSubscriptions]
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [ExpirationDate] DATETIME NOT NULL,
    CONSTRAINT [PK_meetings_MemberSubscriptions_Id] PRIMARY KEY ([Id] ASC)
)
GO

INSERT INTO payments.PriceListItems
VALUES ('d58f0876-efe3-4b4c-b196-a4c3d5fadd24', 'Month', 'New', 'PL', 60, 'PLN', 1)

INSERT INTO payments.PriceListItems
VALUES ('d48e9951-2ae8-467e-a257-a1f492dbd36d', 'HalfYear', 'New', 'PL', 320, 'PLN', 1)

INSERT INTO payments.PriceListItems
VALUES ('b7bbe846-c151-48b5-85ef-a5737108640c', 'Month', 'New', 'US', 15, 'USD', 1)

INSERT INTO payments.PriceListItems
VALUES ('92666bf7-7e86-4784-9c69-e6f3b8bb0ea6', 'HalfYear', 'New', 'US', 80, 'USD', 1)
GO

INSERT INTO payments.PriceListItems
VALUES ('d58f0876-efe3-4b4c-b196-a4c3d5fadd24', 'Month', 'Renewal', 'PL', 60, 'PLN', 1)

INSERT INTO payments.PriceListItems
VALUES ('d48e9951-2ae8-467e-a257-a1f492dbd36d', 'HalfYear', 'Renewal', 'PL', 320, 'PLN', 1)

INSERT INTO payments.PriceListItems
VALUES ('b7bbe846-c151-48b5-85ef-a5737108640c', 'Month', 'Renewal', 'US', 15, 'USD', 1)

INSERT INTO payments.PriceListItems
VALUES ('92666bf7-7e86-4784-9c69-e6f3b8bb0ea6', 'HalfYear', 'Renewal', 'US', 80, 'USD', 1)
GO

CREATE VIEW [meetings].[v_MeetingGroupMembers]
AS
SELECT
    [MeetingGroupMember].MeetingGroupId,
    [MeetingGroupMember].MemberId,
    [MeetingGroupMember].RoleCode
FROM meetings.MeetingGroupMembers AS [MeetingGroupMember]
GO

CREATE TABLE [payments].[MeetingFees]
(
    [MeetingFeeId] UNIQUEIDENTIFIER NOT NULL,
    [PayerId] UNIQUEIDENTIFIER NOT NULL,
    [MeetingId] UNIQUEIDENTIFIER NOT NULL,
    [FeeValue] DECIMAL(18, 2) NOT NULL,
    [FeeCurrency] VARCHAR(50) NOT NULL,
    [Status] VARCHAR(50) NOT NULL,
    CONSTRAINT [PK_payments_MeetingFees_MeetingFeeId] PRIMARY KEY ([MeetingFeeId] ASC)
)
GO