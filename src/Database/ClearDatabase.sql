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

DROP TABLE [meetings].[Meetings]
GO

DROP VIEW [meetings].[v_MeetingGroups]
GO


DROP TABLE [administration].[Members]
GO

DROP VIEW [administration].[v_MeetingGroupProposals]
GO

DROP VIEW [administration].[v_Members]
GO

DROP SCHEMA [administration]
GO

DROP TABLE [meetings].[MeetingNotAttendees]
GO

DROP TABLE [meetings].[MeetingWaitlistMembers]
GO

DROP TABLE [meetings].[Members]
GO

DROP VIEW [meetings].[v_Meetings]
GO

DROP VIEW [meetings].[v_Members]
GO

DROP VIEW [meetings].[v_MeetingGroupProposals]
GO



DROP TABLE payments.InboxMessages

DROP TABLE payments.InternalCommands
GO

DROP TABLE payments.OutboxMessages
GO

DROP TABLE payments.Payers
GO

DROP TABLE users.InboxMessages
GO

DROP TABLE users.InternalCommands
GO

DROP TABLE users.OutboxMessages
GO

DROP TABLE users.[RolesToPermissions]
GO

DROP TABLE users.[Permissions]
GO

DROP TABLE users.[Users]
GO

DROP TABLE users.[UserRoles]
GO

DROP TABLE users.[UserRegistrations]
GO

DROP VIEW users.[v_UserPermissions]
GO

DROP VIEW users.[v_UserRegistrations]
GO

DROP VIEW users.[v_UserRoles]
GO

DROP VIEW users.[v_Users]
GO

DROP SCHEMA [users]
GO

DROP TABLE payments.[Messages]
GO


DROP TABLE payments.Streams
GO


DROP TYPE payments.NewStreamMessages
GO

DROP TABLE payments.SubscriptionDetails
GO

DROP TABLE payments.SubscriptionCheckpoints
GO

DROP TABLE payments.PriceListItems
GO

DROP TABLE payments.SubscriptionPayments
GO

DROP TABLE [meetings].[MemberSubscriptions]
GO

DROP SCHEMA [payments]
GO

DROP VIEW meetings.v_MeetingGroupMembers

DROP SCHEMA [meetings]
GO
