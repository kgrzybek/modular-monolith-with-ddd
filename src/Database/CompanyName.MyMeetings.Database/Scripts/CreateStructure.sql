
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
PRINT N'Creating [usersmi]...';


GO
CREATE SCHEMA [usersmi]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating [payments].[NewStreamMessages]...';


GO
CREATE TYPE [payments].[NewStreamMessages] AS TABLE (
    [StreamVersion] INT              IDENTITY (0, 1) NOT NULL,
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [Created]       DATETIME         DEFAULT (GETUTCDATE()) NOT NULL,
    [Type]          NVARCHAR (128)   NOT NULL,
    [JsonData]      NVARCHAR (MAX)   NULL,
    [JsonMetadata]  NVARCHAR (MAX)   NULL);


GO
PRINT N'Creating [administration].[InternalCommands]...';


GO
CREATE TABLE [administration].[InternalCommands] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [EnqueueDate]   DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    [Error]         NVARCHAR (MAX)   NULL,
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
    [Error]         NVARCHAR (MAX)   NULL,
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
PRINT N'Creating [meetings].[MemberSubscriptions]...';


GO
CREATE TABLE [meetings].[MemberSubscriptions] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ExpirationDate] DATETIME         NOT NULL,
    CONSTRAINT [PK_meetings_MemberSubscriptions_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [meetings].[MeetingAttendees]...';


GO
CREATE TABLE [meetings].[MeetingAttendees] (
    [MeetingId]             UNIQUEIDENTIFIER NOT NULL,
    [AttendeeId]            UNIQUEIDENTIFIER NOT NULL,
    [DecisionDate]          DATETIME2 (7)    NOT NULL,
    [RoleCode]              VARCHAR (50)     NULL,
    [GuestsNumber]          INT              NULL,
    [DecisionChanged]       BIT              NOT NULL,
    [DecisionChangeDate]    DATETIME2 (7)    NULL,
    [IsRemoved]             BIT              NOT NULL,
    [RemovingMemberId]      UNIQUEIDENTIFIER NULL,
    [RemovingReason]        NVARCHAR (500)   NULL,
    [RemovedDate]           DATETIME2 (7)    NULL,
    [BecameNotAttendeeDate] DATETIME2 (7)    NULL,
    [FeeValue]              DECIMAL (5)      NULL,
    [FeeCurrency]           VARCHAR (3)      NULL,
    [IsFeePaid]             BIT              NOT NULL,
    CONSTRAINT [PK_meetings_MeetingAttendees_Id] PRIMARY KEY CLUSTERED ([MeetingId] ASC, [AttendeeId] ASC, [DecisionDate] ASC)
);

GO
PRINT N'Creating [meetings].[MeetingComments]...';


GO
CREATE TABLE meetings.MeetingComments
(
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [MeetingId]             UNIQUEIDENTIFIER NOT NULL,
    [AuthorId]              UNIQUEIDENTIFIER NOT NULL,
    [InReplyToCommentId]    UNIQUEIDENTIFIER NULL,
    [Comment]               VARCHAR(300)     NULL,
    [IsRemoved]             BIT              NOT NULL,
    [RemovedByReason]       VARCHAR(300)     NULL,
    [CreateDate]            DATETIME         NOT NULL,
    [EditDate]              DATETIME         NULL,
    [LikesCount]            INT              NOT NULL,
    CONSTRAINT [PK_meetings_MeetingComments_Id] PRIMARY KEY ([Id] ASC)
)

GO
PRINT N'Creating [meetings].[MeetingMemberCommentLikes]...';


CREATE TABLE [meetings].[MeetingMemberCommentLikes]
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [MemberId]  UNIQUEIDENTIFIER NOT NULL,
    [MeetingCommentId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_meetings_MeetingMemberCommentLikes_Id] PRIMARY KEY ([Id] ASC),
    CONSTRAINT [FK_meetings_MeetingMemberCommentLikes_Members] FOREIGN KEY ([MemberId]) REFERENCES meetings.Members ([Id]),
    CONSTRAINT [FK_meetings_MeetingMemberCommentLikes_MeetingComments] FOREIGN KEY ([MeetingCommentId]) REFERENCES meetings.MeetingComments ([Id])
)

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
    [Error]         NVARCHAR (MAX)   NULL,
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
PRINT N'Creating [payments].[Messages]...';


GO
CREATE TABLE [payments].[Messages] (
    [StreamIdInternal] INT              NOT NULL,
    [StreamVersion]    INT              NOT NULL,
    [Position]         BIGINT           IDENTITY (0, 1) NOT NULL,
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Created]          DATETIME         NOT NULL,
    [Type]             NVARCHAR (128)   NOT NULL,
    [JsonData]         NVARCHAR (MAX)   NOT NULL,
    [JsonMetadata]     NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY NONCLUSTERED ([Position] ASC)
);


GO
PRINT N'Creating [payments].[Messages].[IX_Messages_Position]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Messages_Position]
    ON [payments].[Messages]([Position] ASC);


GO
PRINT N'Creating [payments].[Messages].[IX_Messages_StreamIdInternal_Id]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Messages_StreamIdInternal_Id]
    ON [payments].[Messages]([StreamIdInternal] ASC, [Id] ASC);


GO
PRINT N'Creating [payments].[Messages].[IX_Messages_StreamIdInternal_Revision]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Messages_StreamIdInternal_Revision]
    ON [payments].[Messages]([StreamIdInternal] ASC, [StreamVersion] ASC);


GO
PRINT N'Creating [payments].[Messages].[IX_Messages_StreamIdInternal_Created]...';


GO
CREATE NONCLUSTERED INDEX [IX_Messages_StreamIdInternal_Created]
    ON [payments].[Messages]([StreamIdInternal] ASC, [Created] ASC);


GO
PRINT N'Creating [payments].[MeetingFees]...';


GO
CREATE TABLE [payments].[MeetingFees] (
    [MeetingFeeId] UNIQUEIDENTIFIER NOT NULL,
    [PayerId]      UNIQUEIDENTIFIER NOT NULL,
    [MeetingId]    UNIQUEIDENTIFIER NOT NULL,
    [FeeValue]     DECIMAL (18, 2)  NOT NULL,
    [FeeCurrency]  VARCHAR (50)     NOT NULL,
    [Status]       VARCHAR (50)     NOT NULL,
    CONSTRAINT [PK_payments_MeetingFees_MeetingFeeId] PRIMARY KEY CLUSTERED ([MeetingFeeId] ASC)
);


GO
PRINT N'Creating [payments].[PriceListItems]...';


GO
CREATE TABLE [payments].[PriceListItems] (
    [Id]                     UNIQUEIDENTIFIER NOT NULL,
    [SubscriptionPeriodCode] VARCHAR (50)     NOT NULL,
    [CategoryCode]           VARCHAR (50)     NOT NULL,
    [CountryCode]            VARCHAR (50)     NOT NULL,
    [MoneyValue]             DECIMAL (18, 2)  NOT NULL,
    [MoneyCurrency]          VARCHAR (50)     NOT NULL,
    [IsActive]               BIT              NOT NULL
);


GO
PRINT N'Creating [payments].[SubscriptionPayments]...';


GO
CREATE TABLE [payments].[SubscriptionPayments] (
    [PaymentId]      UNIQUEIDENTIFIER NOT NULL,
    [PayerId]        UNIQUEIDENTIFIER NOT NULL,
    [Type]           VARCHAR (50)     NOT NULL,
    [Status]         VARCHAR (50)     NOT NULL,
    [Period]         VARCHAR (50)     NOT NULL,
    [Date]           DATETIME         NOT NULL,
    [SubscriptionId] UNIQUEIDENTIFIER NULL,
    [MoneyValue]     DECIMAL (18, 2)  NOT NULL,
    [MoneyCurrency]  VARCHAR (50)     NOT NULL
);


GO
PRINT N'Creating [payments].[SubscriptionCheckpoints]...';


GO
CREATE TABLE [payments].[SubscriptionCheckpoints] (
    [Code]     VARCHAR (50) NOT NULL,
    [Position] BIGINT       NOT NULL
);


GO
PRINT N'Creating [payments].[SubscriptionDetails]...';


GO
CREATE TABLE [payments].[SubscriptionDetails] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [Period]         VARCHAR (50)     NOT NULL,
    [Status]         VARCHAR (50)     NOT NULL,
    [CountryCode]    VARCHAR (50)     NOT NULL,
    [ExpirationDate] DATETIME         NOT NULL,
    CONSTRAINT [PK_payments_SubscriptionDetails_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [payments].[Streams]...';


GO
CREATE TABLE [payments].[Streams] (
    [Id]         CHAR (42)       NOT NULL,
    [IdOriginal] NVARCHAR (1000) NOT NULL,
    [IdInternal] INT             IDENTITY (1, 1) NOT NULL,
    [Version]    INT             NOT NULL,
    [Position]   BIGINT          NOT NULL,
    CONSTRAINT [PK_Streams] PRIMARY KEY CLUSTERED ([IdInternal] ASC)
);


GO
PRINT N'Creating [payments].[Streams].[IX_Streams_Id]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Streams_Id]
    ON [payments].[Streams]([Id] ASC);


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
    [Error]         NVARCHAR (MAX)   NULL,
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
PRINT N'Tabelle "[usersmi].[Permissions]" wird erstellt...';


GO
CREATE TABLE [usersmi].[Permissions] (
    [Code]        VARCHAR (100) NOT NULL,
    [Name]        VARCHAR (100) NOT NULL,
    [Description] VARCHAR (255) NULL,
    CONSTRAINT [PK_Permission_Code] PRIMARY KEY CLUSTERED ([Code] ASC)
);


GO
PRINT N'Tabelle "[usersmi].[Roles]" wird erstellt...';


GO
CREATE TABLE [usersmi].[Roles] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Name]             NVARCHAR (256)   NULL,
    [NormalizedName]   NVARCHAR (256)   NULL,
    [ConcurrencyStamp] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Role_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_Role_Name] UNIQUE NONCLUSTERED ([Name] ASC),
    CONSTRAINT [UQ_Role_NormalizedName] UNIQUE NONCLUSTERED ([NormalizedName] ASC)
);


GO
PRINT N'Tabelle "[usersmi].[RoleClaims]" wird erstellt...';


GO
CREATE TABLE [usersmi].[RoleClaims] (
    [Id]         INT              IDENTITY (1, 1) NOT NULL,
    [RoleId]     UNIQUEIDENTIFIER NOT NULL,
    [ClaimType]  NVARCHAR (MAX)   NULL,
    [ClaimValue] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_RoleClaim_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Tabelle "[usersmi].[Users]" wird erstellt...';


GO
CREATE TABLE [usersmi].[Users] (
    [Id]                   UNIQUEIDENTIFIER   NOT NULL,
    [Name]                 NVARCHAR (255)     NULL,
    [FirstName]            NVARCHAR (100)     NULL,
    [LastName]             NVARCHAR (100)     NULL,
    [UserName]             NVARCHAR (256)     NULL,
    [NormalizedUserName]   NVARCHAR (256)     NULL,
    [Email]                NVARCHAR (256)     NULL,
    [NormalizedEmail]      NVARCHAR (256)     NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [PhoneNumber]          NVARCHAR (MAX)     NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_User_NormalizedUserName] UNIQUE NONCLUSTERED ([NormalizedUserName] ASC),
    CONSTRAINT [UQ_User_UserName] UNIQUE NONCLUSTERED ([UserName] ASC)
);


GO
PRINT N'Tabelle "[usersmi].[UserClaims]" wird erstellt...';


GO
CREATE TABLE [usersmi].[UserClaims] (
    [Id]         INT              IDENTITY (1, 1) NOT NULL,
    [UserId]     UNIQUEIDENTIFIER NOT NULL,
    [ClaimType]  NVARCHAR (MAX)   NULL,
    [ClaimValue] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_UserClaim_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Tabelle "[usersmi].[UserLogins]" wird erstellt...';


GO
CREATE TABLE [usersmi].[UserLogins] (
    [LoginProvider]       NVARCHAR (450)   NOT NULL,
    [ProviderKey]         NVARCHAR (450)   NOT NULL,
    [ProviderDisplayName] NVARCHAR (MAX)   NULL,
    [UserId]              UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UserLogin_LoginProvider_ProviderKey] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC)
);


GO
PRINT N'Tabelle "[usersmi].[UserRefreshTokens]" wird erstellt...';


GO
CREATE TABLE [usersmi].[UserRefreshTokens] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [UserId]     UNIQUEIDENTIFIER NOT NULL,
    [Token]      NVARCHAR (MAX)   NOT NULL,
    [JwtId]      NVARCHAR (MAX)   NOT NULL,
    [IsRevoked]  BIT              NOT NULL,
    [AddedDate]  DATETIME2 (7)    NOT NULL,
    [ExpiryDate] DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_UserRefreshToken_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Tabelle "[usersmi].[UserRoles]" wird erstellt...';


GO
CREATE TABLE [usersmi].[UserRoles] (
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UserRole_UserId_RoleId] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC)
);


GO
PRINT N'Tabelle "[usersmi].[UserTokens]" wird erstellt...';


GO
CREATE TABLE [usersmi].[UserTokens] (
    [UserId]        UNIQUEIDENTIFIER NOT NULL,
    [LoginProvider] NVARCHAR (450)   NOT NULL,
    [Name]          NVARCHAR (450)   NOT NULL,
    [Value]         NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_UserToken_UserId_LoginProvider_Name] PRIMARY KEY CLUSTERED ([UserId] ASC, [LoginProvider] ASC, [Name] ASC)
);


GO
PRINT N'Tabelle "[usersmi].[InboxMessages]" wird erstellt...';


GO
CREATE TABLE [usersmi].[InboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_InboxMessages_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Tabelle "[usersmi].[InternalCommands]" wird erstellt...';


GO
CREATE TABLE [usersmi].[InternalCommands] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [EnqueueDate]   DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    [Error]         NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_InternalCommands_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Tabelle "[usersmi].[OutboxMessages]" wird erstellt...';


GO
CREATE TABLE [usersmi].[OutboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_OutboxMessages_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Tabelle "[usersmi].[UserRegistrations]" wird erstellt...';


GO
CREATE TABLE [usersmi].[UserRegistrations] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [UserName]      NVARCHAR (100)   NOT NULL,
    [Email]         NVARCHAR (255)   NOT NULL,
    [Password]      NVARCHAR (255)   NOT NULL,
    [FirstName]     NVARCHAR (50)    NOT NULL,
    [LastName]      NVARCHAR (50)    NOT NULL,
    [Name]          NVARCHAR (255)   NOT NULL,
    [StatusCode]    VARCHAR (50)     NOT NULL,
    [RegisterDate]  DATETIME         NOT NULL,
    [ConfirmedDate] DATETIME         NULL,
    CONSTRAINT [PK_UserRegistrations_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Fremdschlüssel "[usersmi].[FK_RoleClaim_RoleId_Role_Id]" wird erstellt...';


GO
ALTER TABLE [usersmi].[RoleClaims] WITH NOCHECK
    ADD CONSTRAINT [FK_RoleClaim_RoleId_Role_Id] FOREIGN KEY ([RoleId]) REFERENCES [usersmi].[Roles] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Fremdschlüssel "[usersmi].[FK_UserClaim_UserId_User_Id]" wird erstellt...';


GO
ALTER TABLE [usersmi].[UserClaims] WITH NOCHECK
    ADD CONSTRAINT [FK_UserClaim_UserId_User_Id] FOREIGN KEY ([UserId]) REFERENCES [usersmi].[Users] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Fremdschlüssel "[usersmi].[FK_UserLogin_UserId_User_Id]" wird erstellt...';


GO
ALTER TABLE [usersmi].[UserLogins] WITH NOCHECK
    ADD CONSTRAINT [FK_UserLogin_UserId_User_Id] FOREIGN KEY ([UserId]) REFERENCES [usersmi].[Users] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Fremdschlüssel "[usersmi].[FK_UserRefreshToken_UserId_User_Id]" wird erstellt...';


GO
ALTER TABLE [usersmi].[UserRefreshTokens] WITH NOCHECK
    ADD CONSTRAINT [FK_UserRefreshToken_UserId_User_Id] FOREIGN KEY ([UserId]) REFERENCES [usersmi].[Users] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Fremdschlüssel "[usersmi].[FK_UserRole_RoleId_Role_Id]" wird erstellt...';


GO
ALTER TABLE [usersmi].[UserRoles] WITH NOCHECK
    ADD CONSTRAINT [FK_UserRole_RoleId_Role_Id] FOREIGN KEY ([RoleId]) REFERENCES [usersmi].[Roles] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Fremdschlüssel "[usersmi].[FK_UserRole_UserId_User_Id]" wird erstellt...';


GO
ALTER TABLE [usersmi].[UserRoles] WITH NOCHECK
    ADD CONSTRAINT [FK_UserRole_UserId_User_Id] FOREIGN KEY ([UserId]) REFERENCES [usersmi].[Users] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Fremdschlüssel "[usersmi].[FK_UserToken_UserId_User_Id]" wird erstellt...';


GO
ALTER TABLE [usersmi].[UserTokens] WITH NOCHECK
    ADD CONSTRAINT [FK_UserToken_UserId_User_Id] FOREIGN KEY ([UserId]) REFERENCES [usersmi].[Users] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [payments].[DF_payments_Streams_Version]...';


GO
ALTER TABLE [payments].[Streams]
    ADD CONSTRAINT [DF_payments_Streams_Version] DEFAULT (-1) FOR [Version];


GO
PRINT N'Creating [payments].[DF_payments_Streams_Position]...';


GO
ALTER TABLE [payments].[Streams]
    ADD CONSTRAINT [DF_payments_Streams_Position] DEFAULT (-1) FOR [Position];


GO
PRINT N'Creating [payments].[FK_Events_Streams]...';


GO
ALTER TABLE [payments].[Messages] WITH NOCHECK
    ADD CONSTRAINT [FK_Events_Streams] FOREIGN KEY ([StreamIdInternal]) REFERENCES [payments].[Streams] ([IdInternal]);


GO
PRINT N'Creating [administration].[v_MeetingGroupProposals]...';


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
PRINT N'Creating [administration].[v_Members]...';


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
PRINT N'Creating [meetings].[v_MeetingGroupMembers]...';


GO
CREATE VIEW [meetings].[v_MeetingGroupMembers]
AS
SELECT
    [MeetingGroupMember].MeetingGroupId,
    [MeetingGroupMember].MemberId,
    [MeetingGroupMember].RoleCode
FROM meetings.MeetingGroupMembers AS [MeetingGroupMember]
GO
PRINT N'Creating [meetings].[v_MeetingGroupProposals]...';


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
PRINT N'Creating [users].[v_UserRegistrations]...';


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

PRINT N'Sicht "[usersmi].[v_UserPermissions]" wird erstellt...';


GO
CREATE VIEW [usersmi].[v_UserPermissions]
AS
SELECT 
	DISTINCT
	[UserRole].[UserId]				 AS [UserId],
	[RoleClaim].[ClaimValue]		 AS [PermissionCode]
FROM [usersmi].UserRoles AS [UserRole]
	 INNER JOIN [usersmi].[RoleClaims] AS [RoleClaim]
			 ON [UserRole].[RoleId] = [RoleClaim].[RoleId]
GO
PRINT N'Sicht "[usersmi].[v_UserRegistrations]" wird erstellt...';


GO
CREATE VIEW [usersmi].[v_UserRegistrations]
AS
SELECT
    [UserRegistration].[Id],
    [UserRegistration].[UserName],
    [UserRegistration].[Email],
    [UserRegistration].[FirstName],
    [UserRegistration].[LastName],
    [UserRegistration].[Name],
    [UserRegistration].[StatusCode]
FROM [usersmi].[UserRegistrations] AS [UserRegistration]
GO
PRINT N'Sicht "[usersmi].[v_UserRoles]" wird erstellt...';


GO
CREATE VIEW [usersmi].[v_UserRoles]
AS
SELECT
    [UserRole].[UserId],
    [Role].[Name]
FROM [usersmi].[UserRoles] AS [UserRole]
	 INNER JOIN [usersmi].[Roles] AS [Role]
		     ON [UserRole].[UserId] = [Role].[Id]
GO
PRINT N'Sicht "[usersmi].[v_Users]" wird erstellt...';


GO
CREATE VIEW [usersmi].[v_Users]
AS
SELECT
    [User].[Id],
    IIF([User].[LockoutEnabled] = 1, 0, 1) AS [IsActive],
    [User].[UserName] AS [Login],
    [User].[PasswordHash] AS [Password],
    [User].[Email],
    [User].[Name]
FROM [usersmi].[Users] AS [User]
GO
PRINT N'Update abgeschlossen.';


GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [payments].[Messages] WITH CHECK CHECK CONSTRAINT [FK_Events_Streams];


GO
PRINT N'Update complete.';


GO
