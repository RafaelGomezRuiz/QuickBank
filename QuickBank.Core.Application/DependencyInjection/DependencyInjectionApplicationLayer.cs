using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using QuickBank.Core.Application.Interfaces.Services;
using QuickBank.Core.Application.Services;

namespace QuickBank.Core.Application.DependencyInjection
{
    public static class DependencyInjectionApplicationLayer
    {
        public static void AddApplicationDependency(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
        }
    }
}
