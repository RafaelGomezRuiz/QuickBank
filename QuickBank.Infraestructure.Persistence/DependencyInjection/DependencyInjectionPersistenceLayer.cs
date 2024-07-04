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
                string? connectionString = configuration.GetConnectionStringWithEnviormentVariable("SqlServerConnection", "DOTNET_SERVER_NAME");

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
            services.AddTransient<IPayLogRepository, PayLogRepository>();
            services.AddTransient<ITransferLogRepository, TransferLogRepository>();
            services.AddTransient<ICreditCardRepository, CreditCardRepository>();
            services.AddTransient<ILoanRepository, LoanRepository>();
            services.AddTransient<ISavingAccountRepository, SavingAccountRepository>();
            services.AddTransient<IBeneficeRepository, BeneficeRepository>();
            #endregion
        }
    }
}
