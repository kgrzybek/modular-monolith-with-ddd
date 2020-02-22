CREATE VIEW [payments].[v_MeetingGroupPayments]
AS
SELECT
    [MeetingGroupPayment].[Id],
    [MeetingGroupPayment].[MeetingGroupPaymentRegisterId],
    [MeetingGroupPayment].[Date],
    [MeetingGroupPayment].[PaymentTermStartDate],
    [MeetingGroupPayment].[PaymentTermEndDate],
    [MeetingGroupPayment].[PayerId]
FROM [payments].[MeetingGroupPayments] AS [MeetingGroupPayment]
