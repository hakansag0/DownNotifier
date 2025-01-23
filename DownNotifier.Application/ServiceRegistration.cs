using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace DownNotifier.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationInjections(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));


        }
    }
}
