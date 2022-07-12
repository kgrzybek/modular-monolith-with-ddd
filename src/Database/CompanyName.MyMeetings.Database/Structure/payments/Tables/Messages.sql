CREATE TABLE payments.[Messages]
(
    StreamIdInternal    INT                                     NOT NULL,
    StreamVersion       INT                                     NOT NULL,
    Position            BIGINT                 IDENTITY(0,1)    NOT NULL,
    Id                  UNIQUEIDENTIFIER                        NOT NULL,
    Created             DATETIME                                NOT NULL,
    [Type]              NVARCHAR(128)                           NOT NULL,
    JsonData            NVARCHAR(max)                           NOT NULL,
    JsonMetadata        NVARCHAR(max)                                   ,
    CONSTRAINT PK_Events PRIMARY KEY NONCLUSTERED (Position),
    CONSTRAINT FK_Events_Streams FOREIGN KEY (StreamIdInternal) REFERENCES payments.Streams(IdInternal)
);
GO

CREATE UNIQUE NONCLUSTERED INDEX IX_Messages_Position ON payments.Messages (Position);
GO

CREATE UNIQUE NONCLUSTERED INDEX IX_Messages_StreamIdInternal_Id ON payments.Messages (StreamIdInternal, Id);
GO

CREATE UNIQUE NONCLUSTERED INDEX IX_Messages_StreamIdInternal_Revision ON payments.Messages (StreamIdInternal, StreamVersion);
GO

CREATE NONCLUSTERED INDEX IX_Messages_StreamIdInternal_Created ON payments.Messages (StreamIdInternal, Created);
GO
