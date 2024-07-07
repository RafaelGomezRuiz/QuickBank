using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using QuickBank.Core.Application.Interfaces.Services.Commons;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using QuickBank.Core.Application.Interfaces.Services.Logs;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.Interfaces.Services.User;
using QuickBank.Core.Application.Services.Commons;
using QuickBank.Core.Application.Services.Facilities;
using QuickBank.Core.Application.Services.Log;
using QuickBank.Core.Application.Services.Products;
using QuickBank.Core.Application.Services.User;

namespace QuickBank.Core.Application.DependencyInjection
{
    public static class DependencyInjectionApplicationLayer
    {
        public static void AddApplicationDependency(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IBeneficeService, BeneficeService>();
            services.AddTransient<ILoanService, LoanService>();
            services.AddTransient<ICreditCardService, CreditCardService>();
            services.AddTransient<ISavingAccountService, SavingAccountService>();
            services.AddTransient<IPayService, PayService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPayValidationService, PayValidationService>();
        }
    }
}
