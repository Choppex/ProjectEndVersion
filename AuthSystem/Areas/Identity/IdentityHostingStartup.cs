using System;
using AuthSystem.Areas.Identity.Data;
using AuthSystem.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(AuthSystem.Areas.Identity.IdentityHostingStartup))]
namespace AuthSystem.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((services) =>
            {
                //services.AddDbContext<AuthSystemContext>(options =>
                //    options.UseSqlServer(
                //        context.Configuration.GetConnectionString("AuthSystemContextConnection")));

                services.AddIdentity<AuthSystemUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddDefaultUI()
                .AddEntityFrameworkStores<AuthSystemContext>()
                .AddDefaultTokenProviders();

                services.AddAuthorization(options =>
                {
                    options.AddPolicy("adminpolicy",
                        builder => builder.RequireRole("Admin"));
                    options.AddPolicy("standardpolicy",
                        policy => policy.RequireRole("Admin", "Skarbnik", "Komandor", "CzlonekKlubu"));
                    options.AddPolicy("skarbnikpolicy",
                        builder => builder.RequireRole("Admin", "Skarbnik"));
                    options.AddPolicy("komandorpolicy",
                        builder => builder.RequireRole("Admin", "Komandor"));
                });

                //services.AddDefaultIdentity<AuthSystemUser>(options =>
                //{
                //    options.SignIn.RequireConfirmedAccount = false;
                //    options.Password.RequireNonAlphanumeric = false;
                //    options.Password.RequireUppercase = false;
                //    options.Password.RequireLowercase = false;
                //})
                //    .AddEntityFrameworkStores<AuthSystemContext>();
            });
        }
    }
}
