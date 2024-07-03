using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickBank.Core.Application.Interfaces.Services;
using QuickBank.Core.Domain.Settings;
using QuickBank.Infraestructure.Shared.Services;

namespace QuickBank.Infrastructure.Shared.DependencyInjection
{
    public static class DependencyInjectionSharedLayer
    {
        public static void AddSharedDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
