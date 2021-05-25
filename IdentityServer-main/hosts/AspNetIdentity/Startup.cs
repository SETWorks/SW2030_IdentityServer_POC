using Duende.IdentityServer;
using Host.AspNetIdentity.Models;
using IdentityServerHost;
using IdentityServerHost.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Host.AspNetIdentity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddIdentityServer()
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
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
                    b.UseSqlServer(connectionString, dbOpts => dbOpts.MigrationsAssembly(typeof(Startup).Assembly.FullName));                    
                };

                

            })
            .AddOperationalStore(options =>
            {
                options.DefaultSchema = "Identity";

                options.DeviceFlowCodes.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.DeviceFlowCodes);
                // options..key.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.ApiScope);
                options.PersistedGrants.Name = nameof(Duende.IdentityServer.EntityFramework.Entities.PersistedGrant);

                options.ConfigureDbContext = b =>
                {
                    b.UseSqlServer(connectionString, dbOpts => dbOpts.MigrationsAssembly(typeof(Startup).Assembly.FullName));
                };

                options.EnableTokenCleanup = true;
            })
                //.AddConfigurationStore(options =>
                //{
                //    options.DefaultSchema = "Identity";

                //    options.ConfigureDbContext = b =>
                //        b.UseSqlServer(connectionString, dbOpts => dbOpts.MigrationsAssembly(typeof(Startup).Assembly.FullName));
                //})
                //// this adds the operational data from DB (codes, tokens, consents)
                //.AddOperationalStore(options =>
                //{
                //    options.DefaultSchema = "Identity";

                //    options.ConfigureDbContext = b =>
                //        b.UseSqlServer(connectionString, dbOpts => dbOpts.MigrationsAssembly(typeof(Startup).Assembly.FullName));

                //    // this enables automatic token cleanup. this is optional.
                //    options.EnableTokenCleanup = true;
                //})
                .AddProfileService<ProfileService>()
                ;

            services.AddAuthentication()
                .AddOpenIdConnect("Google", "Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ForwardSignOut = IdentityServerConstants.DefaultCookieAuthenticationScheme;

                    options.Authority = "https://accounts.google.com/";
                    options.ClientId = "708996912208-9m4dkjb5hscn7cjrn5u0r4tbgkbj1fko.apps.googleusercontent.com";

                    options.CallbackPath = "/signin-google";
                    options.Scope.Add("email");
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}