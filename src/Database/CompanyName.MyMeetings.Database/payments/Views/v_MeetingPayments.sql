CREATE VIEW [payments].[v_MeetingPayments]
AS
SELECT
    [MeetingPayment].[PayerId],
    [MeetingPayment].[MeetingId],
    [MeetingPayment].[CreateDate],
    [MeetingPayment].[PaymentDate],
    [MeetingPayment].[FeeValue],
    [MeetingPayment].[FeeCurrency]
FROM [payments].[MeetingPayments] AS [MeetingPayment]
