﻿IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Identity].[ApiResources] (
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
    CONSTRAINT [PK_ApiResources] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Identity].[ApiScopes] (
    [Id] int NOT NULL IDENTITY,
    [Enabled] bit NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    [DisplayName] nvarchar(200) NULL,
    [Description] nvarchar(1000) NULL,
    [Required] bit NOT NULL,
    [Emphasize] bit NOT NULL,
    [ShowInDiscoveryDocument] bit NOT NULL,
    CONSTRAINT [PK_ApiScopes] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Identity].[Clients] (
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
    CONSTRAINT [PK_Clients] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Identity].[IdentityResources] (
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
    CONSTRAINT [PK_IdentityResources] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Identity].[ApiResourceClaims] (
    [Id] int NOT NULL IDENTITY,
    [Type] nvarchar(200) NOT NULL,
    [ApiResourceId] int NOT NULL,
    CONSTRAINT [PK_ApiResourceClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApiResourceClaims_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [Identity].[ApiResources] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Identity].[ApiResourceProperties] (
    [Id] int NOT NULL IDENTITY,
    [Key] nvarchar(250) NOT NULL,
    [Value] nvarchar(2000) NOT NULL,
    [ApiResourceId] int NOT NULL,
    CONSTRAINT [PK_ApiResourceProperties] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApiResourceProperties_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [Identity].[ApiResources] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Identity].[ApiResourceScopes] (
    [Id] int NOT NULL IDENTITY,
    [Scope] nvarchar(200) NOT NULL,
    [ApiResourceId] int NOT NULL,
    CONSTRAINT [PK_ApiResourceScopes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApiResourceScopes_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [Identity].[ApiResources] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Identity].[ApiResourceSecrets] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(1000) NULL,
    [Value] nvarchar(4000) NOT NULL,
    [Expiration] datetime2 NULL,
    [Type] nvarchar(250) NOT NULL,
    [Created] datetime2 NOT NULL,
    [ApiResourceId] int NOT NULL,
    CONSTRAINT [PK_ApiResourceSecrets] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApiResourceSecrets_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [Identity].[ApiResources] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Identity].[ApiScopeClaims] (
    [Id] int NOT NULL IDENTITY,
    [Type] nvarchar(200) NOT NULL,
    [ScopeId] int NOT NULL,
    CONSTRAINT [PK_ApiScopeClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApiScopeClaims_ApiScopes_ScopeId] FOREIGN KEY ([ScopeId]) REFERENCES [Identity].[ApiScopes] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Identity].[ApiScopeProperties] (
    [Id] int NOT NULL IDENTITY,
    [Key] nvarchar(250) NOT NULL,
    [Value] nvarchar(2000) NOT NULL,
    [ScopeId] int NOT NULL,
    CONSTRAINT [PK_ApiScopeProperties] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApiScopeProperties_ApiScopes_ScopeId] FOREIGN KEY ([ScopeId]) REFERENCES [Identity].[ApiScopes] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Identity].[ClientClaims] (
    [Id] int NOT NULL IDENTITY,
    [Type] nvarchar(250) NOT NULL,
    [Value] nvarchar(250) NOT NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_ClientClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientClaims_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Clients] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Identity].[ClientCorsOrigins] (
    [Id] int NOT NULL IDENTITY,
    [Origin] nvarchar(150) NOT NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_ClientCorsOrigins] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientCorsOrigins_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Clients] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Identity].[ClientGrantTypes] (
    [Id] int NOT NULL IDENTITY,
    [GrantType] nvarchar(250) NOT NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_ClientGrantTypes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientGrantTypes_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Clients] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Identity].[ClientIdPRestrictions] (
    [Id] int NOT NULL IDENTITY,
    [Provider] nvarchar(200) NOT NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_ClientIdPRestrictions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientIdPRestrictions_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Clients] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Identity].[ClientPostLogoutRedirectUris] (
    [Id] int NOT NULL IDENTITY,
    [PostLogoutRedirectUri] nvarchar(2000) NOT NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_ClientPostLogoutRedirectUris] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientPostLogoutRedirectUris_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Clients] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Identity].[ClientProperties] (
    [Id] int NOT NULL IDENTITY,
    [Key] nvarchar(250) NOT NULL,
    [Value] nvarchar(2000) NOT NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_ClientProperties] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientProperties_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Clients] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Identity].[ClientRedirectUris] (
    [Id] int NOT NULL IDENTITY,
    [RedirectUri] nvarchar(2000) NOT NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_ClientRedirectUris] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientRedirectUris_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Clients] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Identity].[ClientScopes] (
    [Id] int NOT NULL IDENTITY,
    [Scope] nvarchar(200) NOT NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_ClientScopes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientScopes_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Clients] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Identity].[ClientSecrets] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(2000) NULL,
    [Value] nvarchar(4000) NOT NULL,
    [Expiration] datetime2 NULL,
    [Type] nvarchar(250) NOT NULL,
    [Created] datetime2 NOT NULL,
    [ClientId] int NOT NULL,
    CONSTRAINT [PK_ClientSecrets] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClientSecrets_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Identity].[Clients] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Identity].[IdentityResourceClaims] (
    [Id] int NOT NULL IDENTITY,
    [Type] nvarchar(200) NOT NULL,
    [IdentityResourceId] int NOT NULL,
    CONSTRAINT [PK_IdentityResourceClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_IdentityResourceClaims_IdentityResources_IdentityResourceId] FOREIGN KEY ([IdentityResourceId]) REFERENCES [Identity].[IdentityResources] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Identity].[IdentityResourceProperties] (
    [Id] int NOT NULL IDENTITY,
    [Key] nvarchar(250) NOT NULL,
    [Value] nvarchar(2000) NOT NULL,
    [IdentityResourceId] int NOT NULL,
    CONSTRAINT [PK_IdentityResourceProperties] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_IdentityResourceProperties_IdentityResources_IdentityResourceId] FOREIGN KEY ([IdentityResourceId]) REFERENCES [Identity].[IdentityResources] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_ApiResourceClaims_ApiResourceId] ON [Identity].[ApiResourceClaims] ([ApiResourceId]);

GO

CREATE INDEX [IX_ApiResourceProperties_ApiResourceId] ON [Identity].[ApiResourceProperties] ([ApiResourceId]);

GO

CREATE UNIQUE INDEX [IX_ApiResources_Name] ON [Identity].[ApiResources] ([Name]);

GO

CREATE INDEX [IX_ApiResourceScopes_ApiResourceId] ON [Identity].[ApiResourceScopes] ([ApiResourceId]);

GO

CREATE INDEX [IX_ApiResourceSecrets_ApiResourceId] ON [Identity].[ApiResourceSecrets] ([ApiResourceId]);

GO

CREATE INDEX [IX_ApiScopeClaims_ScopeId] ON [Identity].[ApiScopeClaims] ([ScopeId]);

GO

CREATE INDEX [IX_ApiScopeProperties_ScopeId] ON [Identity].[ApiScopeProperties] ([ScopeId]);

GO

CREATE UNIQUE INDEX [IX_ApiScopes_Name] ON [Identity].[ApiScopes] ([Name]);

GO

CREATE INDEX [IX_ClientClaims_ClientId] ON [Identity].[ClientClaims] ([ClientId]);

GO

CREATE INDEX [IX_ClientCorsOrigins_ClientId] ON [Identity].[ClientCorsOrigins] ([ClientId]);

GO

CREATE INDEX [IX_ClientGrantTypes_ClientId] ON [Identity].[ClientGrantTypes] ([ClientId]);

GO

CREATE INDEX [IX_ClientIdPRestrictions_ClientId] ON [Identity].[ClientIdPRestrictions] ([ClientId]);

GO

CREATE INDEX [IX_ClientPostLogoutRedirectUris_ClientId] ON [Identity].[ClientPostLogoutRedirectUris] ([ClientId]);

GO

CREATE INDEX [IX_ClientProperties_ClientId] ON [Identity].[ClientProperties] ([ClientId]);

GO

CREATE INDEX [IX_ClientRedirectUris_ClientId] ON [Identity].[ClientRedirectUris] ([ClientId]);

GO

CREATE UNIQUE INDEX [IX_Clients_ClientId] ON [Identity].[Clients] ([ClientId]);

GO

CREATE INDEX [IX_ClientScopes_ClientId] ON [Identity].[ClientScopes] ([ClientId]);

GO

CREATE INDEX [IX_ClientSecrets_ClientId] ON [Identity].[ClientSecrets] ([ClientId]);

GO

CREATE INDEX [IX_IdentityResourceClaims_IdentityResourceId] ON [Identity].[IdentityResourceClaims] ([IdentityResourceId]);

GO

CREATE INDEX [IX_IdentityResourceProperties_IdentityResourceId] ON [Identity].[IdentityResourceProperties] ([IdentityResourceId]);

GO

CREATE UNIQUE INDEX [IX_IdentityResources_Name] ON [Identity].[IdentityResources] ([Name]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210113185538_Configuration', N'3.1.0');

GO

