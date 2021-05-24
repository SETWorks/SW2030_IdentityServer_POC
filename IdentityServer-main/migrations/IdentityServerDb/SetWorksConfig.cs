// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using Duende.IdentityServer.Models;
using Microsoft.Extensions.Configuration;

namespace SqlServer
{
    public static class SetWorksConfig
    {
        public static IConfiguration Configuration { get; set; }

        public static void Init(IConfiguration config)
        {
            Configuration = config;
        }

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {

                new ApiScope("scope1"),
                new ApiScope("scope2"),
                new ApiScope("SetWorksAPI"),
                new ApiScope("WeatherApi"),

            };
        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("WeatherApi", "The Weather API"),
                new ApiResource("SetWorksAPI", "Set-works API"),
            };
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "setworks.blazorClient",
                    ClientName = "setworks code Client for API",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    //RedirectUris = { Configuration["BlazorClientAddress"] + "/signin-oidc" },
                    //PostLogoutRedirectUris = { Configuration["BlazorClientAddress"] + "/signout-callback-oidc" },

                    RedirectUris = { Configuration["BlazorClientAddress"] + "/authentication/login-callback" },
                    PostLogoutRedirectUris = { Configuration["BlazorClientAddress"] + "/authentication/logout-callback" },

                    //RedirectUris = { "https://localhost:5020/authentication/login-callback" },
                    //PostLogoutRedirectUris = { "https://localhost:5020/authentication/logout-callback" }

                    AllowedCorsOrigins = { Configuration["BlazorClientAddress"] },

                    AllowedScopes = { "openid", "profile", "SetWorksAPI", "WeatherApi" },
                },
                // new Client
                // {
                //     ClientId = "SetWorks.Cms.Demo.WasmApp",
                //     ClientName = "SetWorks Cms Demo WasmApp",
                //
                //     AllowedGrantTypes = GrantTypes.Code,
                //     RequirePkce = true,
                //     RequireClientSecret = false,
                //
                //     //RedirectUris = { Configuration["BlazorClientAddress"] + "/signin-oidc" },
                //     //PostLogoutRedirectUris = { Configuration["BlazorClientAddress"] + "/signout-callback-oidc" },
                //
                //     RedirectUris = { Configuration["SetWorks.Cms.Demo.WasmApp.BaseUrl"] + "/authentication/login-callback" },
                //     PostLogoutRedirectUris = { Configuration["SetWorks.Cms.Demo.WasmApp.BaseUrl"] + "/authentication/logout-callback" },
                //
                //     //RedirectUris = { "https://localhost:5020/authentication/login-callback" },
                //     //PostLogoutRedirectUris = { "https://localhost:5020/authentication/logout-callback" }
                //
                //     AllowedCorsOrigins = { Configuration["SetWorks.Cms.Demo.WasmApp.BaseUrl"] },
                //
                //     
                //     AllowedScopes = { "openid", "profile", "SetWorksAPI", "WeatherApi" },
                // },
                new Client
                {
                    ClientId = "SetWorks.Cms.Demo.WasmApp",
                    ClientName = "SetWorks.Cms.Demo.WasmApp",

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    //RedirectUris = { Configuration["BlazorClientAddress"] + "/signin-oidc" },
                    //PostLogoutRedirectUris = { Configuration["BlazorClientAddress"] + "/signout-callback-oidc" },

                    RedirectUris = { Configuration["SetWorks.Cms.Demo.WasmApp.BaseUrl"] + "/signin-oidc" },
                    //FrontChannelLogoutUri = "https://localhost:5002/signout-oidc",
                    BackChannelLogoutUri = Configuration["SetWorks.Cms.Demo.WasmApp.BaseUrl"] + "/bff/backchannel",
                    PostLogoutRedirectUris = { Configuration["SetWorks.Cms.Demo.WasmApp.BaseUrl"] + "/signout-callback-oidc" },

                    //RedirectUris = { "https://localhost:5020/authentication/login-callback" },
                    //PostLogoutRedirectUris = { "https://localhost:5020/authentication/logout-callback" }
                    
                    AllowOfflineAccess = true,
                    // AllowedCorsOrigins = { Configuration["SetWorks.Cms.Demo.Bff.CoreHostedApp.BaseUrl"] },
                    AllowedScopes = { "openid", "profile", "scope1", "scope2", "SetWorksAPI", "WeatherApi" },
                },
                new Client
                {
                    ClientId = "SetWorks.Cms.Demo.WebApp",
                    ClientName = "SetWorks CMS Demo Web App",

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    //RedirectUris = { Configuration["BlazorClientAddress"] + "/signin-oidc" },
                    //PostLogoutRedirectUris = { Configuration["BlazorClientAddress"] + "/signout-callback-oidc" },
                    
                    RedirectUris = { Configuration["SetWorks.Cms.Demo.WebApp.BaseUrl"] + "/signin-oidc" },
                    BackChannelLogoutUri = Configuration["SetWorks.Cms.Demo.WebApp.BaseUrl"] + "/bff/backchannel",
                    PostLogoutRedirectUris = { Configuration["SetWorks.Cms.Demo.WebApp.BaseUrl"] + "/signout-callback-oidc" },
                    // FrontChannelLogoutUri = Configuration["ClientAddress"] + "/signout-oidc",

                    //RedirectUris = { Configuration["BlazorClientAddress"] + "/authentication/login-callback" },
                    //PostLogoutRedirectUris = { Configuration["BlazorClientAddress"] + "/authentication/logout-callback" },

                    //RedirectUris = { "https://localhost:5020/authentication/login-callback" },
                    //PostLogoutRedirectUris = { "https://localhost:5020/authentication/logout-callback" }

                    AllowedCorsOrigins = { Configuration["SetWorks.Cms.Demo.WebApp.BaseUrl"] },
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "scope1", "scope2", "SetWorksAPI", "WeatherApi" },
                    
                    // AlwaysIncludeUserClaimsInIdToken = true
                },
                new Client
                {
                    ClientId = "setworks.blazorServer",
                    ClientName = "setworks code Server for API",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    //RedirectUris = { Configuration["BlazorClientAddress"] + "/signin-oidc" },
                    //PostLogoutRedirectUris = { Configuration["BlazorClientAddress"] + "/signout-callback-oidc" },
                    
                    RedirectUris = { Configuration["BlazorServerAddress"] + "/signin-oidc" },
                    PostLogoutRedirectUris = { Configuration["BlazorServerAddress"] + "/signout-callback-oidc" },
                    // FrontChannelLogoutUri = Configuration["ClientAddress"] + "/signout-oidc",

                    //RedirectUris = { Configuration["BlazorClientAddress"] + "/authentication/login-callback" },
                    //PostLogoutRedirectUris = { Configuration["BlazorClientAddress"] + "/authentication/logout-callback" },

                    //RedirectUris = { "https://localhost:5020/authentication/login-callback" },
                    //PostLogoutRedirectUris = { "https://localhost:5020/authentication/logout-callback" }

                    AllowedCorsOrigins = { Configuration["BlazorServerAddress"] },

                    AllowedScopes = { "openid", "profile", "SetWorksAPI", "WeatherApi" },
                },
                // setworks client credentials flow client
                new Client
                {
                    ClientId = "setworks.client",
                    ClientName = "setworks code Client for API",

                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = { new Secret("ac769c6e-95d0-4dab-a05a-ddef32d57ba5".Sha256()) },

                    RedirectUris = { Configuration["ClientAddress"] + "/signin-oidc" },
                    PostLogoutRedirectUris = { Configuration["ClientAddress"] + "/signout-callback-oidc" },
                    FrontChannelLogoutUri = Configuration["ClientAddress"] + "/signout-oidc",

                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    AllowedScopes = { "openid", "profile", "scope1", "scope2", "SetWorksAPI" },
                    //Claims = { new ClientClaim("tenant", "Wise"), new ClientClaim("tenant", "Ohio") }
                },
                new Client
                {
                    ClientId = "SetWorks.Cms.WebApi",
                    ClientName = "SetWorks CMS WebApi",

                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = { new Secret("adac92a1-9792-4ee3-a752-caffc450bf94".Sha256()) },

                    RedirectUris = { Configuration["SetWorks.Cms.WebApi.BaseUrl"] + "/signin-oidc" },
                    PostLogoutRedirectUris = { Configuration["SetWorks.Cms.WebApi.BaseUrl"] + "/signout-callback-oidc" },
                    FrontChannelLogoutUri = Configuration["SetWorks.Cms.WebApi.BaseUrl"] + "/signout-oidc",

                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    AllowedScopes = { "openid", "profile", "scope1", "scope2", "SetWorksAPI" },
                    //Claims = { new ClientClaim("tenant", "Wise"), new ClientClaim("tenant", "Ohio") }
                },
                
                
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "scope1" }
                },
                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { Configuration["ClientAddress"] + "/signin-oidc" },
                    FrontChannelLogoutUri = Configuration["ClientAddress"] + "/signout-oidc",
                    PostLogoutRedirectUris = { Configuration["ClientAddress"] + "/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "scope2" }
                },
            };
    }
}
