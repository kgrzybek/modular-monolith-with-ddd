DELETE FROM [administration].[InboxMessages]

DELETE FROM [administration].[InternalCommands]

DELETE FROM [administration].[OutboxMessages]

DELETE FROM [administration].[MeetingGroupProposals]

DELETE FROM [administration].[Members]

DELETE FROM [meetings].[InboxMessages]

DELETE FROM [meetings].[InternalCommands]

DELETE FROM [meetings].[OutboxMessages]

DELETE FROM [meetings].[MeetingAttendees]

DELETE FROM [meetings].[MeetingGroupMembers]

DELETE FROM [meetings].[MeetingGroupProposals]

DELETE FROM [meetings].[MeetingGroups]

DELETE FROM [meetings].[MeetingNotAttendees]

DELETE FROM [meetings].[Meetings]

DELETE FROM [meetings].[MeetingWaitlistMembers]

DELETE FROM [meetings].[MeetingMemberCommentLikes]

DELETE FROM [meetings].[Members]

DELETE FROM [meetings].[MeetingComments]

DELETE FROM [payments].[InboxMessages]

DELETE FROM [payments].[InternalCommands]

DELETE FROM [payments].[OutboxMessages]

DELETE FROM payments.[Messages]

DBCC CHECKIDENT ('payments.Messages', RESEED, 0);

DELETE FROM payments.Streams

DBCC CHECKIDENT ('payments.Streams', RESEED, 0);

DELETE FROM payments.SubscriptionDetails

DELETE FROM [payments].[SubscriptionCheckpoints]

DELETE FROM [payments].PriceListItems

DELETE FROM [payments].SubscriptionPayments

DELETE FROM [payments].MeetingFees

DELETE FROM [payments].[Payers]

DELETE FROM [users].[InboxMessages]

DELETE FROM [users].[InternalCommands]

DELETE FROM [users].[OutboxMessages]

DELETE FROM [users].[Users]

DELETE FROM [users].[RolesToPermissions]

DELETE FROM [users].[UserRoles]

DELETE FROM [users].[Permissions]

DELETE FROM [registrations].[UserRegistrations]