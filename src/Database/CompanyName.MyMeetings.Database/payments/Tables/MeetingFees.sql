CREATE TABLE [payments].[MeetingFees]
(
    [MeetingFeeId] UNIQUEIDENTIFIER NOT NULL,
    [PayerId] UNIQUEIDENTIFIER NOT NULL,
    [MeetingId] UNIQUEIDENTIFIER NOT NULL,
    [FeeValue] DECIMAL(18, 2) NOT NULL,
    [FeeCurrency] VARCHAR(50) NOT NULL,
    [Status] VARCHAR(50) NOT NULL,
    CONSTRAINT [PK_payments_MeetingFees_MeetingFeeId] PRIMARY KEY ([MeetingFeeId] ASC)
)
GO