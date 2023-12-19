-- Global administrator
INSERT INTO [usersmi].[Users] ([Id], [Name], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
	 VALUES
			('39C63EC6-9AFF-473D-010F-08DBFFF6E9DF',
			'Administrator',
			NULL,
			NULL,
			'administrator',
			'ADMINISTRATOR',
			'administrator@mymeetings.com',
			'ADMINISTRATOR@MYMEETINGS.COM',
			1,
			'AQAAAAIAAYagAAAAEMWzWgeHA5LEvzLC9ke6LfqO/ju2RvcguKHjDONHn/CZuOaXwk4WMg8CXP2OOKLHsw==', -- AdminP@$$123.
			'CELIHCK47VKN4IL3ZGTYRVL4WX7AKTS6',
			'60c49c98-2932-43b0-9363-099906901875',
			NULL,
			0,
			0,
			NULL,
			0,
			0);

-- Assign permission to administrator
INSERT INTO [usersmi].[UserClaims] 
			([ClaimType], [ClaimValue], [UserId])
	 VALUES ('Application.Permission', 'Application.Administrator', '39C63EC6-9AFF-473D-010F-08DBFFF6E9DF');


-- Test users
INSERT INTO [usersmi].[UserRegistrations]
			([Id], [UserName], [Email], [Password], [FirstName], [LastName], [Name], [StatusCode], [RegisterDate], [ConfirmedDate])
	 VALUES
			('2EBFECFC-ED13-43B8-B516-6AC89D51C510', 'testMember@mail.com', 'testMember@mail.com', 'testMemberPass', 'John', 'Doe', 'John Doe', 'Confirmed', GETDATE(), GETDATE()),
			('4065630E-4A4C-4F01-9142-0BACF6B8C64D', 'testAdmin@mail.com', 'testAdmin@mail.com', 'testAdminPass', 'Jane', 'Doe', 'Jane Doe', 'Confirmed', GETDATE(), GETDATE());

INSERT INTO [usersmi].[Users]
			([Id], [Name], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
	 VALUES			
			('2EBFECFC-ED13-43B8-B516-6AC89D51C510', 'John Doe', 'John', 'Doe', 'testMember@mail.com', 'TESTMEMBER@MAIL.COM', 'testMember@mail.com', 'TESTMEMBER@MAIL.COM', 0, 'AQAAAAIAAYagAAAAEBRXigjUOJjPf/7wwJV1YlCOk2MzBOne1zLXXQoHzZNVqTnl5UuQWwM94ORZqOTR3g==', 'NMMJRCDE4JDHMJB32D43US57X5WALBPV', 'b7eb06fb-a904-492e-9717-6771cb6ad4af', NULL, 0, 0, NULL, 1, 0),
			('4065630E-4A4C-4F01-9142-0BACF6B8C64D', 'Jane Doe', 'Jane', 'Doe', 'testAdmin@mail.com', 'TESTADMIN@MAIL.COM', 'testAdmin@mail.com', 'TESTADMIN@MAIL.COM', 0, 'AQAAAAIAAYagAAAAEFjQQOh8ii3LbgV0+0C69GCmygp0I8Z9FV0n6SQCFRK+bN/uFkd5ksBjbWNSiz9/Ag==', 'FTD34O4LXMPZFBMJ4DGS5LN2VVUNCHU5', '29467032-0908-4bd8-9ef5-62676a4e5009', NULL, 0, 0, NULL, 1, 0);


-- Assign Roles to Users
INSERT INTO [usersmi].[UserRoles]
			([UserId], [RoleId])
	 VALUES
			('2EBFECFC-ED13-43B8-B516-6AC89D51C510', '9CE09287-D003-439F-42A0-08DC00AA0515'), -- John Doe / Member
			('4065630E-4A4C-4F01-9142-0BACF6B8C64D', '1E5A7F47-A258-4127-42A1-08DC00AA0515'); -- Jane Doe / Administrator