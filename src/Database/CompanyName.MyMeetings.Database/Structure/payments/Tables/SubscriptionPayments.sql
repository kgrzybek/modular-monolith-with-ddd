CREATE TABLE payments.SubscriptionPayments
(
    [PaymentId] UNIQUEIDENTIFIER NOT NULL,
    [PayerId] UNIQUEIDENTIFIER NOT NULL,
    [Type] VARCHAR(50) NOT NULL,
    [Status] VARCHAR(50) NOT NULL,
    [Period] VARCHAR(50) NOT NULL,
    [Date] DATETIME NOT NULL,
    [SubscriptionId] UNIQUEIDENTIFIER NULL,
    [MoneyValue] DECIMAL(18, 2) NOT NULL,
    [MoneyCurrency] VARCHAR(50) NOT NULL
)