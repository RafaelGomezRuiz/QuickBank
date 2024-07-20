using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Services.User;
using QuickBank.Infrastructure.Identity.Context;
using QuickBank.Infrastructure.Identity.Entities;
using QuickBank.Infrastructure.Identity.Seeds;
using QuickBank.Infrastructure.Identity.Services;
using QuickBank.Infrastructure.Persistence.Seeds.Users;
using System.Reflection;

namespace QuickBank.Infrastructure.Identity.DependencyInjection
{
    public static class DependencyInjectionIdentityLayer
    {
        public static void AddIdentityDependency(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options =>
                {
                    options.UseInMemoryDatabase("IdentityContextInMemory");
                });
            }
            else
            {
                var connectionString = configuration.GetConnectionString("IdentityConnection");

                services.AddDbContext<IdentityContext>(options =>
                {
                    options.UseSqlServer(connectionString, a => a.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                });
            }
            #endregion

            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
                //falta el de acceso denegado

                options.AccessDeniedPath = "/Auth/AccessDenied";
            });

            services.AddAuthentication();
            #endregion

            #region Services
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IAccountService, AccountService>();
            #endregion
        }

        public static async Task AddIdentitySeeds(this IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await DefaultRoles.SeedAsync(roleManager);

                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    await DefaultBasicUsers.SeedAsync(userManager);
                    await DefaultAdminUsers.SeedAsync(userManager);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
