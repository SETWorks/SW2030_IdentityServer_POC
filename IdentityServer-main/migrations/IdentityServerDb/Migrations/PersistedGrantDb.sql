IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'Identity') IS NULL EXEC(N'CREATE SCHEMA [Identity];');
GO

CREATE TABLE [Identity].[DeviceFlowCodes] (
    [UserCode] nvarchar(200) NOT NULL,
    [DeviceCode] nvarchar(200) NOT NULL,
    [SubjectId] nvarchar(200) NULL,
    [SessionId] nvarchar(100) NULL,
    [ClientId] nvarchar(200) NOT NULL,
    [Description] nvarchar(200) NULL,
    [CreationTime] datetime2 NOT NULL,
    [Expiration] datetime2 NOT NULL,
    [Data] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_DeviceFlowCodes] PRIMARY KEY ([UserCode])
);
GO

CREATE TABLE [Identity].[Keys] (
    [Id] nvarchar(450) NOT NULL,
    [Version] int NOT NULL,
    [Created] datetime2 NOT NULL,
    [Use] nvarchar(450) NULL,
    [Algorithm] nvarchar(100) NOT NULL,
    [IsX509Certificate] bit NOT NULL,
    [DataProtected] bit NOT NULL,
    [Data] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Keys] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Identity].[PersistedGrant] (
    [Key] nvarchar(200) NOT NULL,
    [Type] nvarchar(50) NOT NULL,
    [SubjectId] nvarchar(200) NULL,
    [SessionId] nvarchar(100) NULL,
    [ClientId] nvarchar(200) NOT NULL,
    [Description] nvarchar(200) NULL,
    [CreationTime] datetime2 NOT NULL,
    [Expiration] datetime2 NULL,
    [ConsumedTime] datetime2 NULL,
    [Data] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_PersistedGrant] PRIMARY KEY ([Key])
);
GO

CREATE UNIQUE INDEX [IX_DeviceFlowCodes_DeviceCode] ON [Identity].[DeviceFlowCodes] ([DeviceCode]);
GO

CREATE INDEX [IX_DeviceFlowCodes_Expiration] ON [Identity].[DeviceFlowCodes] ([Expiration]);
GO

CREATE INDEX [IX_Keys_Use] ON [Identity].[Keys] ([Use]);
GO

CREATE INDEX [IX_PersistedGrant_Expiration] ON [Identity].[PersistedGrant] ([Expiration]);
GO

CREATE INDEX [IX_PersistedGrant_SubjectId_ClientId_Type] ON [Identity].[PersistedGrant] ([SubjectId], [ClientId], [Type]);
GO

CREATE INDEX [IX_PersistedGrant_SubjectId_SessionId_Type] ON [Identity].[PersistedGrant] ([SubjectId], [SessionId], [Type]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210525132747_Grants', N'5.0.0');
GO

COMMIT;
GO

