CREATE TABLE [payments].MeetingGroupPayments
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[MeetingGroupPaymentRegisterId] UNIQUEIDENTIFIER NOT NULL,
	[Date] DATETIME NOT NULL,
	[PaymentTermStartDate] DATE NOT NULL,
	[PaymentTermEndDate] DATE NOT NULL,
	[PayerId] UNIQUEIDENTIFIER NOT NULL
)