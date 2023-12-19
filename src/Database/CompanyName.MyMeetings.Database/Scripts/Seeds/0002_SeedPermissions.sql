INSERT INTO [usersmi].[Permissions] ([Code], [Name], [Description])
	 VALUES

			-- ##############################################################################################################
			-- #											 *** APPLICATION ***											#
			-- ##############################################################################################################
			('Application.Administrator',									'Administrator',							NULL),


			-- ##############################################################################################################
			-- #											    *** USERS ***												#
			-- ##############################################################################################################

			-- Identity
			('Users.GetUserAccounts',										'GetUserAccounts',							NULL),

			-- Users
			('Users.GetUsers',												'GetUsers',									NULL),
			('Users.CreateUserAccount',										'CreateUserAccount',						NULL),
			('Users.UpdateUserAccount',										'UpdateUserAccount',						NULL),
			('Users.UnlockUserAccount',										'UnlockUserAccount',						NULL),
			('Users.ConfirmEmailAddress',									'ConfirmEmailAddress',						NULL),
			('Users.GetAuthenticatorKey',									'GetAuthenticatorKey',						NULL),
			('Users.RegisterAuthenticator',									'RegisterAuthenticator',					NULL),
			('Users.GetUserRoles',											'GetUserRoles',								NULL),
			('Users.SetUserRoles',											'SetUserRoles',								NULL),
			('Users.GetUserPermissions',									'GetUserPermissions',						NULL),
			('Users.SetUserPermissions',									'SetUserPermissions',						NULL),
			
			-- User roles
			('Users.GetRoles',												'GetRoles',									NULL),
			('Users.AddRole',												'AddRole',									NULL),
			('Users.RenameRole',											'RenameRole',								NULL),
			('Users.DeleteRole',											'DeleteRole',								NULL),
			('Users.GetRolePermissions',									'GetRolePermissions',						NULL),
			('Users.SetRolePermissions',									'SetRolePermissions',						NULL),

			-- ##############################################################################################################
			-- #											    *** MEETINGS ***											#
			-- ##############################################################################################################
			('GetMeetingGroupProposals',									'GetMeetingGroupProposals',					NULL),
			('ProposeMeetingGroup',											'ProposeMeetingGroup',						NULL),
			('CreateNewMeeting',											'CreateNewMeeting',							NULL),
			('EditMeeting',													'EditMeeting',								NULL),
			('AddMeetingAttendee',											'AddMeetingAttendee',						NULL),
			('RemoveMeetingAttendee',										'RemoveMeetingAttendee',					NULL),
			('AddNotAttendee',												'AddNotAttendee',							NULL),
			('ChangeNotAttendeeDecision',									'ChangeNotAttendeeDecision',				NULL),
			('SignUpMemberToWaitlist',										'SignUpMemberToWaitlist',					NULL),
			('SignOffMemberFromWaitlist',									'SignOffMemberFromWaitlist',				NULL),
			('SetMeetingHostRole',											'SetMeetingHostRole',						NULL),
			('SetMeetingAttendeeRole',										'SetMeetingAttendeeRole',					NULL),
			('CancelMeeting',												'CancelMeeting',							NULL),
			('GetAllMeetingGroups',											'GetAllMeetingGroups',						NULL),
			('EditMeetingGroupGeneralAttributes',							'EditMeetingGroupGeneralAttributes',		NULL),
			('JoinToGroup',													'JoinToGroup',								NULL),
			('LeaveMeetingGroup',											'LeaveMeetingGroup',						NULL),
			('AddMeetingComment',											'AddMeetingComment',						NULL),
			('EditMeetingComment',											'EditMeetingComment',						NULL),
			('RemoveMeetingComment',										'RemoveMeetingComment',						NULL),
			('AddMeetingCommentReply',										'AddMeetingCommentReply',					NULL),
			('LikeMeetingComment',											'LikeMeetingComment',						NULL),
			('UnlikeMeetingComment',										'UnlikeMeetingComment',						NULL),
			('EnableMeetingCommenting',										'EnableMeetingCommenting',					NULL),
			('DisableMeetingCommenting',									'DisableMeetingCommenting',					NULL),
			('MyMeetingGroupsView',											'MyMeetingGroupsView',						NULL),
			('AllMeetingGroupsView',										'AllMeetingGroupsView',						NULL),
			('SubscriptionView',											'SubscriptionView',							NULL),
			('EmailsView',													'EmailsView',								NULL),
			('MyMeetingsView',												'MyMeetingsView',							NULL),
			('GetAuthenticatedMemberMeetings',								'GetAuthenticatedMemberMeetings',			NULL),

			-- ##############################################################################################################
			-- #											 *** ADMINISTRATION ***											#
			-- ##############################################################################################################
			-- 
			('AcceptMeetingGroupProposal',									'AcceptMeetingGroupProposal',				NULL),
			('AdministrationsView',											'AdministrationsView',						NULL),

			-- ##############################################################################################################
			-- #											    *** PAYMENTS ***											#
			-- ##############################################################################################################
			('RegisterPayment',												'RegisterPayment',							NULL),
			('BuySubscription',												'BuySubscription',							NULL),
			('RenewSubscription',											'RenewSubscription',						NULL),
			('CreatePriceListItem',											'CreatePriceListItem',						NULL),
			('ActivatePriceListItem',										'ActivatePriceListItem',					NULL),
			('DeactivatePriceListItem',										'DeactivatePriceListItem',					NULL),
			('ChangePriceListItemAttributes',								'ChangePriceListItemAttributes',			NULL),
			('GetAuthenticatedPayerSubscription',							'GetAuthenticatedPayerSubscription',		NULL),
			('GetPriceListItem',											'GetPriceListItem',							NULL);