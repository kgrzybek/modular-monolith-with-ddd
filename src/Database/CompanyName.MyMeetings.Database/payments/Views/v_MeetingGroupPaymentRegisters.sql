CREATE VIEW [payments].[v_MeetingGroupPaymentRegisters]
AS
SELECT
    [MeetingGroupPaymentRegister].[Id],
    [MeetingGroupPaymentRegister].[CreateDate]
FROM [payments].[MeetingGroupPaymentRegisters] AS [MeetingGroupPaymentRegister]
