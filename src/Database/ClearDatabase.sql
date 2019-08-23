DELETE FROM [administration].[MeetingGroupProposals]
GO

DELETE FROM [administration].[OutboxMessages]
GO

DELETE FROM [administration].[InboxMessages]
GO

DELETE FROM [administration].[InternalCommands]
GO

DELETE FROM [meetings].[InboxMessages]
GO

DELETE FROM [meetings].[OutboxMessages]
GO

DELETE FROM [meetings].[InternalCommands]
GO

DELETE FROM [meetings].[MeetingGroupProposals]
GO

DELETE FROM [meetings].[MeetingGroupMembers]
GO

DELETE FROM [meetings].[MeetingAttendees]
GO

DELETE FROM [meetings].[MeetingGroups]
GO

DROP TABLE [administration].[MeetingGroupProposals]
GO

DROP TABLE [administration].[OutboxMessages]
GO

DROP TABLE [administration].[InboxMessages]
GO

DROP TABLE [administration].[InternalCommands]
GO

DROP TABLE [meetings].[InboxMessages]
GO

DROP TABLE [meetings].[OutboxMessages]
GO

DROP TABLE [meetings].[InternalCommands]
GO

DROP TABLE [meetings].[MeetingGroupProposals]
GO

DROP TABLE [meetings].[MeetingGroupMembers]
GO

DROP TABLE [meetings].[MeetingAttendees]
GO

DROP TABLE [meetings].[MeetingGroups]
GO

DROP TABLE [meetings].[MeetingGroupPayments]
GO

DROP TABLE [meetings].[Meetings]
GO

DROP VIEW [meetings].[v_MeetingGroups]
GO

DROP SCHEMA [administration]
GO

DROP SCHEMA [meetings]
GO