
using System;
using System.Linq;
using System.Security.Claims;
using Host.AspNetIdentity.Models;
using IdentityServerHost.Data;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IdentityServerHost.Models;

namespace IdentityServerHost
{
    public class SeedData
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();

                if (!context.Roles.Any(r => r.Name == "Admin"))
                {
                    context.Roles.Add(new ApplicationRole { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "Admin", ConcurrencyStamp = Guid.NewGuid().ToString() });
                    context.SaveChanges();
                }

                if (!context.Roles.Any(r => r.Name == "Manager"))
                {
                    context.Roles.Add(new ApplicationRole { Id = Guid.NewGuid(), Name = "Manager", NormalizedName = "Manager", ConcurrencyStamp = Guid.NewGuid().ToString() });
                    context.SaveChanges();
                }

                if (!context.Roles.Any(r => r.Name == "Staff"))
                {
                    context.Roles.Add(new ApplicationRole { Id = Guid.NewGuid(), Name = "Staff", NormalizedName = "Staff", ConcurrencyStamp = Guid.NewGuid().ToString() });
                    context.SaveChanges();
                }

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var alice = userMgr.FindByNameAsync("alice").Result;
                if (alice == null)
                {
                    alice = new ApplicationUser
                    {
                        UserName = "alice"
                    };
                    var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(alice, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine("alice created");
                }
                else
                {
                    Console.WriteLine("alice already exists");
                }

                var bob = userMgr.FindByNameAsync("bob").Result;
                if (bob == null)
                {
                    bob = new ApplicationUser
                    {
                        UserName = "bob"
                    };
                    var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(bob, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim("location", "somewhere")
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine("bob created");
                }
                else
                {
                    Console.WriteLine("bob already exists");
                }

                var thao = userMgr.FindByNameAsync("thao").Result;
                if (thao == null)
                {
                    thao = new ApplicationUser
                    {
                        UserName = "thao"
                    };
                    var result = userMgr.CreateAsync(thao, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(thao, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Thao Bui-Bond"),
                        new Claim(JwtClaimTypes.GivenName, "Thao"),
                        new Claim(JwtClaimTypes.FamilyName, "Bui-Bond"),
                        new Claim(JwtClaimTypes.Email, "tbond@set-works.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://thaoslife.com"),
                        new Claim("location", "Kansas City")
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine("thao created");
                }
                else
                {
                    Console.WriteLine("thao already exists");
                }

                var blake = userMgr.FindByNameAsync("blake").Result;
                if (blake == null)
                {
                    blake = new ApplicationUser
                    {
                        UserName = "blake"
                    };
                    var result = userMgr.CreateAsync(blake, "Blake123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(blake, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Blake Theiss"),
                        new Claim(JwtClaimTypes.GivenName, "Blake"),
                        new Claim(JwtClaimTypes.FamilyName, "Theiss"),
                        new Claim(JwtClaimTypes.Email, "btheiss@set-works.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://blakeslife.com"),
                        new Claim("location", "Kansas City")
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine("blake created");
                }
                else
                {


                    Console.WriteLine("blake already exists");
                }

                var adminRole = context.Roles.First(r => r.Name == "Admin");
                if (!context.UserRoles.Any(ur => ur.RoleId == adminRole.Id && ur.UserId == blake.Id))
                {
                    context.UserRoles.Add(new IdentityUserRole<Guid> { RoleId = adminRole.Id, UserId = blake.Id });
                    context.SaveChanges();
                }                

                var henri = userMgr.FindByNameAsync("henri").Result;
                if (henri == null)
                {
                    henri = new ApplicationUser
                    {
                        UserName = "henri"
                    };
                    var result = userMgr.CreateAsync(henri, "Henri123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(henri, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Henri McCracken"),
                        new Claim(JwtClaimTypes.GivenName, "Henri"),
                        new Claim(JwtClaimTypes.FamilyName, "McCracken"),
                        new Claim(JwtClaimTypes.Email, "hmccracken@set-works.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://henrislife.com"),
                        new Claim("location", "Kansas City")
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine("henri created");
                }
                else
                {
                    Console.WriteLine("henri already exists");
                }

                var david = userMgr.FindByNameAsync("david").Result;
                if (david == null)
                {
                    david = new ApplicationUser
                    {
                        UserName = "david"
                    };
                    var result = userMgr.CreateAsync(david, "David123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(david, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "David Lindell"),
                        new Claim(JwtClaimTypes.GivenName, "David"),
                        new Claim(JwtClaimTypes.FamilyName, "Lindell"),
                        new Claim(JwtClaimTypes.Email, "dlindell@set-works.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://davidslife.com"),
                        new Claim("location", "Kansas City")
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine("david created");
                }
                else
                {
                    Console.WriteLine("david already exists");
                }
            }
        }
    }
}
