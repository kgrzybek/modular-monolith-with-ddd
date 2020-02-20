
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
    CONSTRAINT [PK_meetings_MeetingAttendees_Id] PRIMARY KEY CLUSTERED ([MeetingId] ASC, [AttendeeId] ASC, [DecisionDate] ASC)
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
PRINT N'Creating [payments].[MeetingPayments]...';


GO
CREATE TABLE [payments].[MeetingPayments] (
    [PayerId]     UNIQUEIDENTIFIER NOT NULL,
    [MeetingId]   UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]  DATETIME2 (7)    NOT NULL,
    [PaymentDate] DATETIME2 (7)    NULL,
    [FeeValue]    DECIMAL (5)      NOT NULL,
    [FeeCurrency] VARCHAR (3)      NOT NULL,
    CONSTRAINT [PK_meetings_Meetings_Id] PRIMARY KEY CLUSTERED ([PayerId] ASC, [MeetingId] ASC)
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
PRINT N'Creating [payments].[MeetingGroupPayments]...';


GO
CREATE TABLE [payments].[MeetingGroupPayments] (
    [Id]                            UNIQUEIDENTIFIER NOT NULL,
    [MeetingGroupPaymentRegisterId] UNIQUEIDENTIFIER NOT NULL,
    [Date]                          DATETIME         NOT NULL,
    [PaymentTermStartDate]          DATE             NOT NULL,
    [PaymentTermEndDate]            DATE             NOT NULL,
    [PayerId]                       UNIQUEIDENTIFIER NOT NULL
);


GO
PRINT N'Creating [payments].[MeetingGroupPaymentRegisters]...';


GO
CREATE TABLE [payments].[MeetingGroupPaymentRegisters] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [CreateDate] DATETIME         NOT NULL
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

-- Initialize some data

-- Add Test Member
INSERT INTO users.UserRegistrations VALUES 
(
	'2EBFECFC-ED13-43B8-B516-6AC89D51C510',
	'testMember@mail.com',
	'testMember@mail.com',
	'ANO7TKjxh/Dom6LG0dyoQfJciLca+e1itHQ6BZMQYs+aMbKL9MjCv6bq4gy4+MRY0w==', -- testMemberPass
	'John',
	'Doe',
	'John Doe',
	'Confirmed',
	GETDATE(),
	GETDATE()
)

INSERT INTO users.Users VALUES
(
	'2EBFECFC-ED13-43B8-B516-6AC89D51C510',
	'testMember@mail.com',
	'testMember@mail.com',
	'ANO7TKjxh/Dom6LG0dyoQfJciLca+e1itHQ6BZMQYs+aMbKL9MjCv6bq4gy4+MRY0w==', -- testMemberPass
	1,
	'John',
	'Doe',
	'John Doe'
)

INSERT INTO users.UserRoles VALUES
('2EBFECFC-ED13-43B8-B516-6AC89D51C510', 'Member')

-- Add Test Administrator
INSERT INTO users.UserRegistrations VALUES 
(
	'4065630E-4A4C-4F01-9142-0BACF6B8C64D',
	'testAdmin@mail.com',
	'testAdmin@mail.com',
	'AK0qplH5peUHwnCVuzW9zy0JGZTTG6/Ji88twX+nw9JdTUwqa3Wol1K4m5aCG9pE2A==', -- testAdminPass
	'Jane',
	'Doe',
	'Jane Doe',
	'Confirmed',
	GETDATE(),
	GETDATE()
)

INSERT INTO users.Users VALUES
(
	'4065630E-4A4C-4F01-9142-0BACF6B8C64D',
	'testAdmin@mail.com',
	'testAdmin@mail.com',
	'AK0qplH5peUHwnCVuzW9zy0JGZTTG6/Ji88twX+nw9JdTUwqa3Wol1K4m5aCG9pE2A==', -- testAdminPass
	1,
	'Jane',
	'Doe',
	'Jane Doe'
)

INSERT INTO users.UserRoles VALUES
('4065630E-4A4C-4F01-9142-0BACF6B8C64D', 'Administrator')

-- Roles to Permissions

INSERT INTO users.[Permissions] (Code, Name) VALUES
(
	-- Meetings
	'ProposeMeetingGroup', 'ProposeMeetingGroup'),
	('CreateNewMeeting','CreateNewMeeting'),
	('EditMeeting','EditMeeting'),
	('AddMeetingAttendee','AddMeetingAttendee'),
	('RemoveMeetingAttendee','RemoveMeetingAttendee'),
	('AddNotAttendee','AddNotAttendee'),
	('ChangeNotAttendeeDecision','ChangeNotAttendeeDecision'),
	('SignUpMemberToWaitlist','SignUpMemberToWaitlist'),
	('SignOffMemberFromWaitlist','SignOffMemberFromWaitlist'),
	('SetMeetingHostRole','SetMeetingHostRole'),
	('SetMeetingAttendeeRole','SetMeetingAttendeeRole'),
	('CancelMeeting','CancelMeeting'),
	('GetAllMeetingGroups','GetAllMeetingGroups'),
	('EditMeetingGroupGeneralAttributes','EditMeetingGroupGeneralAttributes'),
	('JoinToGroup','JoinToGroup'),
	('LeaveMeetingGroup','LeaveMeetingGroup'),

	-- Administration
	('AcceptMeetingGroupProposal','AcceptMeetingGroupProposal'),

	-- Payments
	('RegisterPayment','RegisterPayment')


INSERT INTO users.RolesToPermissions VALUES ('Member', 'ProposeMeetingGroup')
INSERT INTO users.RolesToPermissions VALUES ('Member', 'CreateNewMeeting')
INSERT INTO users.RolesToPermissions VALUES ('Member', 'EditMeeting')
INSERT INTO users.RolesToPermissions VALUES ('Member', 'AddMeetingAttendee')
INSERT INTO users.RolesToPermissions VALUES ('Member', 'RemoveMeetingAttendee')
INSERT INTO users.RolesToPermissions VALUES ('Member', 'AddNotAttendee')
INSERT INTO users.RolesToPermissions VALUES ('Member', 'ChangeNotAttendeeDecision')
INSERT INTO users.RolesToPermissions VALUES ('Member', 'SignUpMemberToWaitlist')
INSERT INTO users.RolesToPermissions VALUES ('Member', 'SignOffMemberFromWaitlist')
INSERT INTO users.RolesToPermissions VALUES ('Member', 'SetMeetingHostRole')
INSERT INTO users.RolesToPermissions VALUES ('Member', 'SetMeetingAttendeeRole')
INSERT INTO users.RolesToPermissions VALUES ('Member', 'CancelMeeting')
INSERT INTO users.RolesToPermissions VALUES ('Member', 'GetAllMeetingGroups')
INSERT INTO users.RolesToPermissions VALUES ('Member', 'EditMeetingGroupGeneralAttributes')
INSERT INTO users.RolesToPermissions VALUES ('Member', 'JoinToGroup')
INSERT INTO users.RolesToPermissions VALUES ('Member', 'LeaveMeetingGroup')

INSERT INTO users.RolesToPermissions VALUES ('Member', 'RegisterPayment')

INSERT INTO users.RolesToPermissions VALUES ('Administrator', 'AcceptMeetingGroupProposal')
