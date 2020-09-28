DELETE FROM payments.SubscriptionDetails
GO

ALTER TABLE payments.SubscriptionDetails ADD [PayerId] UNIQUEIDENTIFIER NOT NULL
GO