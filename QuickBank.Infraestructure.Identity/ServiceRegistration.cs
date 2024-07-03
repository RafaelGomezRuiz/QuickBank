using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickBank.Core.Application.Interfaces.Services;
using QuickBank.Infraestructure.Identity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBank.Infraestructure.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {

            #region Services
            services.AddTransient<IAccountService, AccountService>();
            #endregion
        }
    }
}
