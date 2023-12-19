INSERT INTO [usersmi].[Roles]
			([Id], [Name], [NormalizedName], [ConcurrencyStamp])
	 VALUES
			('1E5A7F47-A258-4127-42A1-08DC00AA0515', 'Administrator', 'ADMINISTRATOR', 'ff5d947e-723d-4bf6-b6b9-0c1487c72f73'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Member', 'MEMBER', 'ad88d3f5-37ef-473e-addc-9ee10184e7d0');


INSERT INTO [usersmi].[RoleClaims]
			([RoleId], [ClaimType], [ClaimValue])
	 VALUES
			-- Administator
			('1E5A7F47-A258-4127-42A1-08DC00AA0515', 'Application.Permission', 'AcceptMeetingGroupProposal'),
			('1E5A7F47-A258-4127-42A1-08DC00AA0515', 'Application.Permission', 'AdministrationsView'),
			('1E5A7F47-A258-4127-42A1-08DC00AA0515', 'Application.Permission', 'CreatePriceListItem'),
			('1E5A7F47-A258-4127-42A1-08DC00AA0515', 'Application.Permission', 'ActivatePriceListItem'),
			('1E5A7F47-A258-4127-42A1-08DC00AA0515', 'Application.Permission', 'DeactivatePriceListItem'),
			('1E5A7F47-A258-4127-42A1-08DC00AA0515', 'Application.Permission', 'ChangePriceListItemAttributes'),
			('1E5A7F47-A258-4127-42A1-08DC00AA0515', 'Application.Permission', 'GetPriceListItem'),
			('1E5A7F47-A258-4127-42A1-08DC00AA0515', 'Application.Permission', 'Users.GetUserAccounts'),

			-- Member
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'GetMeetingGroupProposals'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'ProposeMeetingGroup'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'CreateNewMeeting'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'EditMeeting'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'AddMeetingAttendee'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'RemoveMeetingAttendee'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'AddNotAttendee'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'ChangeNotAttendeeDecision'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'SignUpMemberToWaitlist'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'SignOffMemberFromWaitlist'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'SetMeetingHostRole'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'SetMeetingAttendeeRole'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'CancelMeeting'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'GetAllMeetingGroups'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'EditMeetingGroupGeneralAttributes'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'JoinToGroup'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'LeaveMeetingGroup'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'AddMeetingComment'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'EditMeetingComment'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'RemoveMeetingComment'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'AddMeetingCommentReply'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'LikeMeetingComment'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'UnlikeMeetingComment'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'GetAuthenticatedMemberMeetingGroups'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'GetMeetingGroupDetails'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'GetMeetingDetails'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'GetMeetingAttendees'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'MyMeetingsGroupsView'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'SubscriptionView'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'EmailsView'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'AllMeetingGroupsView'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'MyMeetingsView'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'GetAuthenticatedMemberMeetings'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'RegisterPayment'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'BuySubscription'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'RenewSubscription'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'GetAuthenticatedPayerSubscription'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'GetPriceListItem'),
			('9CE09287-D003-439F-42A0-08DC00AA0515', 'Application.Permission', 'Users.GetUserAccounts');