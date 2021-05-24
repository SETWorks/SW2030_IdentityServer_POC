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

CREATE TABLE [Identity].[ApiResource] (
    [Id] int NOT NULL IDENTITY,
    [Enabled] bit NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    [DisplayName] nvarchar(200) NULL,
    [Description] nvarchar(1000) NULL,
    [AllowedAccessTokenSigningAlgorithms] nvarchar(100) NULL,
    [ShowInDiscoveryDocument] bit NOT NULL,
    [RequireResourceIndicator] bit NOT NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NULL,
    [LastAccessed] datetime2 NULL,
    [NonEditable] bit NOT NULL,
    CONSTRAINT [PK_ApiResource] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Identity].[ApiScope] (
    [Id] int NOT NULL IDENTITY,
    [Enabled] bit NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    [DisplayName] nvarchar(200) NULL,
    [Description] nvarchar(1000) NULL,
    [Required] bit NOT NULL,
    [Emphasize] bit NOT NULL,
    [ShowInDiscoveryDocument] bit NOT NULL,
    CONSTRAINT [PK_ApiScope] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Identity].[Client] (
    [Id] int NOT NULL IDENTITY,
    [Enabled] bit NOT NULL,
    [ClientId] nvarchar(200) NOT NULL,
    [ProtocolType] nvarchar(200) NOT NULL,
    [RequireClientSecret] bit NOT NULL,
    [ClientName] nvarchar(200) NULL,
    [Description] nvarchar(1000) NULL,
    [ClientUri] nvarchar(2000) NULL,
    [LogoUri] nvarchar(2000) NULL,
    [RequireConsent] bit NOT NULL,
    [AllowRememberConsent] bit NOT NULL,
    [AlwaysIncludeUserClaimsInIdToken] bit NOT NULL,
    [RequirePkce] bit NOT NULL,
    [AllowPlainTextPkce] bit NOT NULL,
    [RequireRequestObject] bit NOT NULL,
    [AllowAccessTokensViaBrowser] bit NOT NULL,
    [FrontChannelLogoutUri] nvarchar(2000) NULL,
    [FrontChannelLogoutSessionRequired] bit NOT NULL,
    [BackChannelLogoutUri] nvarchar(2000) NULL,
    [BackChannelLogoutSessionRequired] bit NOT NULL,
    [AllowOfflineAccess] bit NOT NULL,
    [IdentityTokenLifetime] int NOT NULL,
    [AllowedIdentityTokenSigningAlgorithms] nvarchar(100) NULL,
    [AccessTokenLifetime] int NOT NULL,
    [AuthorizationCodeLifetime] int NOT NULL,
    [ConsentLifetime] int NULL,
    [AbsoluteRefreshTokenLifetime] int NOT NULL,
    [SlidingRefreshTokenLifetime] int NOT NULL,
    [RefreshTokenUsage] int NOT NULL,
    [UpdateAccessTokenClaimsOnRefresh] bit NOT NULL,
    [RefreshTokenExpiration] int NOT NULL,
    [AccessTokenType] int NOT NULL,
    [EnableLocalLogin] bit NOT NULL,
    [IncludeJwtId] bit NOT NULL,
    [AlwaysSendClientClaims] bit NOT NULL,
    [ClientClaimsPrefix] nvarchar(200) NULL,
    [PairWiseSubjectSalt] nvarchar(200) NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NULL,
    [LastAccessed] datetime2 NULL,
    [UserSsoLifetime] int NULL,
    [UserCodeType] nvarchar(100) NULL,
    [DeviceCodeLifetime] int NOT NULL,
    [NonEditable] bit NOT NULL,
    CONSTRAINT [PK_Client] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Identity].[IdentityResource] (
    [Id] int NOT NULL IDENTITY,
    [Enabled] bit NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    [DisplayName] nvarchar(200) NULL,
    [Description] nvarchar(1000) NULL,
    [Required] bit NOT NULL,
    [Emphasize] bit NOT NULL,
    [ShowInDiscoveryDocument] bit NOT NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NULL,
    [NonEditable] bit NOT NULL,
    CONSTRAINT [PK_IdentityResource] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Identity].[ApiResourceClaim] (
    [Id] int NOT NULL IDENTITY,
    [ApiResourceId] int NOT NULL,
    [Type] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_ApiResourceClaim] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApiResourceClaim_ApiResource_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [Identity].[ApiResource] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[ApiResourceProperty] (
    [Id] int NOT NULL IDENTITY,
    [ApiResourceId] int NOT NULL,
    [Key] nvarchar(250) NOT NULL,
    [Value] nvarchar(2000) NOT NULL,
    CONSTRAINT [PK_ApiResourceProperty] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApiResourceProperty_ApiResource_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [Identity].[ApiResource] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[ApiResourceScope] (
    [Id] int NOT NULL IDENTITY,
    [Scope] nvarchar(200) NOT NULL,
    [ApiResourceId] int NOT NULL,
    CONSTRAINT [PK_ApiResourceScope] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApiResourceScope_ApiResource_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [Identity].[ApiResource] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[ApiResourceSecret] (
    [Id] int NOT NULL IDENTITY,
    [ApiResourceId] int NOT NULL,
    [Description] nvarchar(1000) NULL,
    [Value] nvarchar(4000) NOT NULL,
    [Expiration] datetime2 NULL,
    [Type] nvarchar(250) NOT NULL,
    [Created] datetime2 NOT NULL,
    CONSTRAINT [PK_ApiResourceSecret] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApiResourceSecret_ApiResource_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [Identity].[ApiResource] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[ApiScopeClaim] (
    [Id] int NOT NULL IDENTITY,
    [ScopeId] int NOT NULL,
    [Type] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_ApiScopeClaim] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApiScopeClaim_ApiScope_ScopeId] FOREIGN KEY ([ScopeId]) REFERENCES [Identity].[ApiScope] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[ApiScopeProperty] (
    [Id] int NOT NULL IDENTITY,
    [ScopeId] int NOT NULL,
    [Key] nvarchar(250) NOT NULL,
    [Value] nvarchar(2000) NOT NULL,
    CONSTRAINT [PK_ApiScopeProperty] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApiScopeProperty_ApiScope_ScopeId] FOREIGN KEY ([ScopeId]) REFERENCES [Identity].[ApiScope] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[ClientClaim] (
    [Id] int NOT NULL IDENTITY,
    [Type] nvarchar(250) NOT NULL,
    [Value] nvarchar(250) NOT NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_ClientClaim] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientClaim_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Client] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[ClientCorsOrigin] (
    [Id] int NOT NULL IDENTITY,
    [Origin] nvarchar(150) NOT NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_ClientCorsOrigin] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientCorsOrigin_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Client] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[ClientGrantType] (
    [Id] int NOT NULL IDENTITY,
    [GrantType] nvarchar(250) NOT NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_ClientGrantType] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientGrantType_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Client] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[ClientIdPRestriction] (
    [Id] int NOT NULL IDENTITY,
    [Provider] nvarchar(200) NOT NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_ClientIdPRestriction] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientIdPRestriction_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Client] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[ClientPostLogoutRedirectUri] (
    [Id] int NOT NULL IDENTITY,
    [PostLogoutRedirectUri] nvarchar(2000) NOT NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_ClientPostLogoutRedirectUri] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientPostLogoutRedirectUri_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Client] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[ClientProperty] (
    [Id] int NOT NULL IDENTITY,
    [ClientId] int NOT NULL,
    [Key] nvarchar(250) NOT NULL,
    [Value] nvarchar(2000) NOT NULL,
    CONSTRAINT [PK_ClientProperty] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientProperty_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Client] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[ClientRedirectUri] (
    [Id] int NOT NULL IDENTITY,
    [RedirectUri] nvarchar(2000) NOT NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_ClientRedirectUri] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientRedirectUri_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Client] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[ClientScope] (
    [Id] int NOT NULL IDENTITY,
    [Scope] nvarchar(200) NOT NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_ClientScope] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientScope_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Client] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[ClientSecret] (
    [Id] int NOT NULL IDENTITY,
    [ClientId] int NOT NULL,
    [Description] nvarchar(2000) NULL,
    [Value] nvarchar(4000) NOT NULL,
    [Expiration] datetime2 NULL,
    [Type] nvarchar(250) NOT NULL,
    [Created] datetime2 NOT NULL,
    CONSTRAINT [PK_ClientSecret] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientSecret_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Client] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[IdentityResourceClaim] (
    [Id] int NOT NULL IDENTITY,
    [IdentityResourceId] int NOT NULL,
    [Type] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_IdentityResourceClaim] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_IdentityResourceClaim_IdentityResource_IdentityResourceId] FOREIGN KEY ([IdentityResourceId]) REFERENCES [Identity].[IdentityResource] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[IdentityResourceProperty] (
    [Id] int NOT NULL IDENTITY,
    [IdentityResourceId] int NOT NULL,
    [Key] nvarchar(250) NOT NULL,
    [Value] nvarchar(2000) NOT NULL,
    CONSTRAINT [PK_IdentityResourceProperty] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_IdentityResourceProperty_IdentityResource_IdentityResourceId] FOREIGN KEY ([IdentityResourceId]) REFERENCES [Identity].[IdentityResource] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_ApiResource_Name] ON [Identity].[ApiResource] ([Name]);
GO

CREATE INDEX [IX_ApiResourceClaim_ApiResourceId] ON [Identity].[ApiResourceClaim] ([ApiResourceId]);
GO

CREATE INDEX [IX_ApiResourceProperty_ApiResourceId] ON [Identity].[ApiResourceProperty] ([ApiResourceId]);
GO

CREATE INDEX [IX_ApiResourceScope_ApiResourceId] ON [Identity].[ApiResourceScope] ([ApiResourceId]);
GO

CREATE INDEX [IX_ApiResourceSecret_ApiResourceId] ON [Identity].[ApiResourceSecret] ([ApiResourceId]);
GO

CREATE UNIQUE INDEX [IX_ApiScope_Name] ON [Identity].[ApiScope] ([Name]);
GO

CREATE INDEX [IX_ApiScopeClaim_ScopeId] ON [Identity].[ApiScopeClaim] ([ScopeId]);
GO

CREATE INDEX [IX_ApiScopeProperty_ScopeId] ON [Identity].[ApiScopeProperty] ([ScopeId]);
GO

CREATE UNIQUE INDEX [IX_Client_ClientId] ON [Identity].[Client] ([ClientId]);
GO

CREATE INDEX [IX_ClientClaim_ClientId] ON [Identity].[ClientClaim] ([ClientId]);
GO

CREATE INDEX [IX_ClientCorsOrigin_ClientId] ON [Identity].[ClientCorsOrigin] ([ClientId]);
GO

CREATE INDEX [IX_ClientGrantType_ClientId] ON [Identity].[ClientGrantType] ([ClientId]);
GO

CREATE INDEX [IX_ClientIdPRestriction_ClientId] ON [Identity].[ClientIdPRestriction] ([ClientId]);
GO

CREATE INDEX [IX_ClientPostLogoutRedirectUri_ClientId] ON [Identity].[ClientPostLogoutRedirectUri] ([ClientId]);
GO

CREATE INDEX [IX_ClientProperty_ClientId] ON [Identity].[ClientProperty] ([ClientId]);
GO

CREATE INDEX [IX_ClientRedirectUri_ClientId] ON [Identity].[ClientRedirectUri] ([ClientId]);
GO

CREATE INDEX [IX_ClientScope_ClientId] ON [Identity].[ClientScope] ([ClientId]);
GO

CREATE INDEX [IX_ClientSecret_ClientId] ON [Identity].[ClientSecret] ([ClientId]);
GO

CREATE UNIQUE INDEX [IX_IdentityResource_Name] ON [Identity].[IdentityResource] ([Name]);
GO

CREATE INDEX [IX_IdentityResourceClaim_IdentityResourceId] ON [Identity].[IdentityResourceClaim] ([IdentityResourceId]);
GO

CREATE INDEX [IX_IdentityResourceProperty_IdentityResourceId] ON [Identity].[IdentityResourceProperty] ([IdentityResourceId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210524223125_Configuration', N'5.0.0');
GO

COMMIT;
GO

