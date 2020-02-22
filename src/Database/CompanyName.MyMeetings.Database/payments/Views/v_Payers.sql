CREATE VIEW [payments].[v_Payers]
AS
SELECT
    [Payer].[Id],
    [Payer].[Login],
    [Payer].[Email],
    [Payer].[FirstName],
    [Payer].[LastName],
    [Payer].[Name]
FROM [payments].[Payers] AS [Payer]
