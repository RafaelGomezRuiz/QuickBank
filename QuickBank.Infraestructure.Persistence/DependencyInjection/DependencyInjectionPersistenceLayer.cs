using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Infrastructure.Persistence.Contexts;
using QuickBank.Infrastructure.Persistence.Repositories;


namespace QuickBank.Infrastructure.Persistence.DependencyInjection
{
    public static class DependencyInjectionPersistenceLayer
    {
        public static void AddPersistenceDependency(this IServiceCollection services, IConfiguration configuration)
        {
            #region Database
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(
                    options => options.UseInMemoryDatabase("DbInMemory")
                );
            }
            else
            {
                string? connectionString = configuration.GetConnectionString("SqlServerConnection");

                // ----------------------| THIS WILL BE TEMPORALLY
                string[] connectionStringArray = connectionString.Split('.');
                connectionStringArray[0] += EnvironmentVariablesHelper.GetValue("DOTNET_SERVER_NAME");
                connectionString = string.Join("",connectionStringArray);
                // ----------------------| THIS WILL BE TEMPORALLY

                services.AddDbContext<ApplicationContext>(
                    options => options.UseSqlServer(
                        connectionString,
                        m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)
                    )
                );
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion
        }
    }
}
