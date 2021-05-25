// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer.EntityFramework.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Options;
using Duende.IdentityServer;

namespace SqlServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var cn = Configuration.GetConnectionString("DefaultConnection");//thao

            SetWorksConfig.Init(Configuration);

            services.AddOperationalDbContext(options =>
            {
                options.DefaultSchema = "Identity";

                options.DeviceFlowCodes.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.DeviceFlowCodes);
                // options..key.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ApiScope);
                options.PersistedGrants.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.PersistedGrant);

                options.ConfigureDbContext = b =>
                {
                    b.UseSqlServer(cn, dbOpts => dbOpts.MigrationsAssembly(typeof(Startup).Assembly.FullName));
                };
            });

            services.AddConfigurationDbContext(options =>
            {
                options.DefaultSchema = "Identity";

                options.ApiResource.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ApiResource);
                options.ApiScope.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ApiScope);
                options.Client.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.Client);
                options.ApiScope.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ApiScope);
                options.IdentityResource.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.IdentityResource);
                options.ApiResourceClaim.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ApiResourceClaim);
                options.ApiResourceProperty.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ApiResourceProperty);
                options.ApiResourceScope.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ApiResourceScope);
                options.ApiResourceSecret.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ApiResourceSecret);
                options.ApiScopeClaim.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ApiScopeClaim);
                options.ApiScopeProperty.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ApiScopeProperty);
                options.ClientClaim.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ClientClaim);
                options.ClientCorsOrigin.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ClientCorsOrigin);
                options.ClientGrantType.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ClientGrantType);
                options.ClientIdPRestriction.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ClientIdPRestriction);
                options.ClientPostLogoutRedirectUri.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ClientPostLogoutRedirectUri);
                options.ClientProperty.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ClientProperty);
                options.ClientRedirectUri.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ClientRedirectUri);
                options.ClientScopes.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ClientScope);
                options.ClientSecret.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ClientSecret);
                options.IdentityResourceClaim.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.IdentityResourceClaim);
                options.IdentityResourceProperty.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.IdentityResourceProperty);

                options.ConfigureDbContext = b =>
                {
                    b.UseSqlServer(cn, dbOpts => dbOpts.MigrationsAssembly(typeof(Startup).Assembly.FullName));
                };
            });
        }

        public void Configure(IApplicationBuilder app)
        {
        }
    }
}
