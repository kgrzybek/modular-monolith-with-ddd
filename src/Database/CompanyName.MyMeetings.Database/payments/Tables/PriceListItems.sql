CREATE TABLE payments.PriceListItems
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [SubscriptionPeriodCode] VARCHAR(50) NOT NULL,
    [CategoryCode] VARCHAR(50) NOT NULL,
    [CountryCode] VARCHAR(50) NOT NULL,
    [MoneyValue] DECIMAL(18, 2) NOT NULL,
    [MoneyCurrency] VARCHAR(50) NOT NULL,
    [IsActive] BIT NOT NULL
)