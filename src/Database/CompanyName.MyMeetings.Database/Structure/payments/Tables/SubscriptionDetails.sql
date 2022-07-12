CREATE TABLE payments.SubscriptionDetails
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [PayerId] UNIQUEIDENTIFIER NOT NULL,
    [Period] VARCHAR(50) NOT NULL,
    [Status] VARCHAR(50) NOT NULL,
    [CountryCode] VARCHAR(50) NOT NULL,
    [ExpirationDate] DATETIME NOT NULL,
    CONSTRAINT [PK_payments_SubscriptionDetails_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
)