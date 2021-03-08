// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Collections.Generic;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
//using Duende.IdentityServer.Models;
using IdentityServerHost.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Duende.IdentityServer.Models;
using Duende.IdentityServer;
using IdentityModel;

namespace SqlServer
{
    public class SWSeedData
    {
        static string[] allowedSWScopes =
{
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
            IdentityServerConstants.StandardScopes.Email,
            IdentityServerConstants.StandardScopes.Phone,
            IdentityServerConstants.StandardScopes.Address,
            "Setworks.Admin",
            "Setworks.Staff",
        };
        static string[] allowedWiseScopes =
        {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
            IdentityServerConstants.StandardScopes.Email,
            IdentityServerConstants.StandardScopes.Phone,
            IdentityServerConstants.StandardScopes.Address,
            "Wise.Admin",
            "Wise.Staff",
        };
        static string[] allowedDentCountyScopes =
        {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
            IdentityServerConstants.StandardScopes.Email,
            IdentityServerConstants.StandardScopes.Phone,
            IdentityServerConstants.StandardScopes.Address,
            "DentCounty.Admin",
            "DentCounty.Staff",
            "Dent"
        };
        public static readonly IEnumerable<IdentityResource> SWIdentityResources =
            new[]
            {
                new IdentityResources.Phone(),//thao adds
                new IdentityResources.Address(),//thao adds
                // custom identity resource with some consolidated claims
                new IdentityResource("setworks.profile", new[] { JwtClaimTypes.Name, JwtClaimTypes.Email, "location", JwtClaimTypes.Address }),
                new IdentityResource("custom.profile", new[] { JwtClaimTypes.Name, JwtClaimTypes.Email, "location", JwtClaimTypes.Address })
            };

        public static readonly IEnumerable<ApiScope> SWApiScopes =
            new[]
            {
                new ApiScope("Setworks.Admin"),
                new ApiScope("Setworks.Staff"),
                new ApiScope("Wise.Admin"),
                new ApiScope("Wise.Staff"),
                new ApiScope("DentCounty.Admin"),
                new ApiScope("DentCounty.Staff"),
                new ApiScope("Dent", "DentTransaction")
                {
                    Description = "Dent County transaction",
                }

            };
        public static readonly IEnumerable<ApiResource> SWApiResources =
            new[]
            {
                new ApiResource("urn:Setworks", "Setworks")
                {
                    ApiSecrets = { new Secret("secret".Sha256()) },
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email
                    },
                    Scopes = { "Setworks.Admin", "Setworks.Staff" }
                },
                new ApiResource("urn:Wise", "Wise")
                {
                    ApiSecrets = { new Secret("secret".Sha256()) },
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email
                    },
                    Scopes = { "Wise.Admin", "Wise.Staff" }
                },
                new ApiResource("urn:DentCounty", "DentCounty")
                {
                    ApiSecrets = { new Secret("secret".Sha256()) },
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email
                    },
                    Scopes = { "DentCounty.Admin.Admin", "DentCounty.Admin.Staff" }
                },
                new ApiResource("urn:Dent", "Dent")
                {
                    ApiSecrets = { new Secret("secret".Sha256()) },
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email
                    },
                    Scopes = { "Dent" }
                }
            };
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                //using (var context = scope.ServiceProvider.GetService<PersistedGrantDbContext>())
                //{
                //    context.Database.Migrate();
                //}
                using (var context = scope.ServiceProvider.GetService<ConfigurationDbContext>())
                {
                    EnsureSeedData(context);
                }
            }
        }

        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            Console.WriteLine("Seeding SETWorks data...");


            Console.WriteLine("SetWorks clients being populated");
            foreach (var client in GetSWDatta())
            {
                context.Clients.Add(client.ToEntity());
            }
            context.SaveChanges();

            Console.WriteLine("IdentityResources being populated");
            foreach (var resource in SWIdentityResources)
            {
                context.IdentityResources.Add(resource.ToEntity());
            }
            context.SaveChanges();

            if (!context.ApiResources.Any())
            {
                Console.WriteLine("ApiResources being populated");
                foreach (var resource in SWApiResources)
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("ApiResources already populated");
            }

            Console.WriteLine("Scopes being populated");
            foreach (var resource in SWApiScopes)
            {
                context.ApiScopes.Add(resource.ToEntity());
            }
            context.SaveChanges();

            Console.WriteLine("Done seeding SetWorks data.");
            Console.WriteLine();
        }

        public static IEnumerable<Client> GetSWDatta()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "setworks.mvc",
                    ClientName = "Wise MVC Code Flow",

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())

                    },

                    RequireConsent = true,
                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:44304/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44304/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44304/signout-callback-oidc" },

                    AllowOfflineAccess = true,

                    AllowedScopes = allowedSWScopes
                },
                new Client
                {
                    ClientId = "Wise.jar.jwt",
                    ClientName = "SetWorks MVC Code Flow with JAR/JWT",
                    ClientUri = "http://identityserver.io",

                    ClientSecrets =
                    {
                        new Secret
                        {
                            Type = IdentityServerConstants.SecretTypes.JsonWebKey,
                            Value = "{'e':'AQAB','kid':'ZzAjSnraU3bkWGnnAqLapYGpTyNfLbjbzgAPbbW2GEA','kty':'RSA','n':'wWwQFtSzeRjjerpEM5Rmqz_DsNaZ9S1Bw6UbZkDLowuuTCjBWUax0vBMMxdy6XjEEK4Oq9lKMvx9JzjmeJf1knoqSNrox3Ka0rnxXpNAz6sATvme8p9mTXyp0cX4lF4U2J54xa2_S9NF5QWvpXvBeC4GAJx7QaSw4zrUkrc6XyaAiFnLhQEwKJCwUw4NOqIuYvYp_IXhw-5Ti_icDlZS-282PcccnBeOcX7vc21pozibIdmZJKqXNsL1Ibx5Nkx1F1jLnekJAmdaACDjYRLL_6n3W4wUp19UvzB1lGtXcJKLLkqB6YDiZNu16OSiSprfmrRXvYmvD8m6Fnl5aetgKw'}"
                        }
                    },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireRequestObject = true,

                    RedirectUris = { "https://localhost:44302/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44302/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44302/signout-callback-oidc" },

                    AllowOfflineAccess = true,

                    AllowedScopes = allowedWiseScopes
                },
                new Client
                {
                    ClientId = "dentCounty.code",
                    ClientName = "Dent County MVC Code Flow",
                    ClientUri = "http://identityserver.io",

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RequireConsent = true,
                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:44302/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44302/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44302/signout-callback-oidc" },

                    AllowOfflineAccess = true,

                    AllowedScopes = allowedDentCountyScopes
                },
            };
        }

    }

}
