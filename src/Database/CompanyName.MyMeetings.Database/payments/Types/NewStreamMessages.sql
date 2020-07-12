CREATE TYPE payments.NewStreamMessages AS TABLE (
    StreamVersion       INT IDENTITY(0,1)                       NOT NULL,
    Id                  UNIQUEIDENTIFIER                        NOT NULL,
    Created             DATETIME          DEFAULT(GETUTCDATE()) NOT NULL,
    [Type]              NVARCHAR(128)                           NOT NULL,
    JsonData            NVARCHAR(max)                           NULL,
    JsonMetadata        NVARCHAR(max)                           NULL
);
GO