using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Services;
using QuickBank.Infrastructure.Identity.Context;
using QuickBank.Infrastructure.Identity.Entities;
using QuickBank.Infrastructure.Identity.Seeds;
using QuickBank.Infrastructure.Identity.Services;

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
                var connectionString = configuration.GetConnectionStringWithEnviormentVariable("IdentityConnection","DOTNET_SERVER_NAME");

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
                options.LoginPath = "/user";
                //falta el de acceso denegado
            });

            services.AddAuthentication();
            #endregion

            #region Services
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
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
