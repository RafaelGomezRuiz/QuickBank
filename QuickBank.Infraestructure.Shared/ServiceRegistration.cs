using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickBank.Core.Application.Interfaces.Services;
using QuickBank.Core.Domain.Settings;
using QuickBank.Infraestructure.Shared.Services;

namespace QuickBank.Infraestructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
