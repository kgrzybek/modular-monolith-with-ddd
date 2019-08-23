CREATE TABLE payments.MeetingPayments
(
    [PayerId] UNIQUEIDENTIFIER NOT NULL,
    [MeetingId] UNIQUEIDENTIFIER NOT NULL,
    [CreateDate] DATETIME2 NOT NULL,
    [PaymentDate] DATETIME2 NULL,
    [FeeValue] DECIMAL(5, 0) NOT NULL,
    [FeeCurrency] VARCHAR(3) NOT NULL,
	CONSTRAINT [PK_meetings_Meetings_Id] PRIMARY KEY ([PayerId] ASC, [MeetingId] ASC)
)
GO