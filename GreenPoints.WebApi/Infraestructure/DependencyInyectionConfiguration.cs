using GreenPoints.Data;
using GreenPoints.Domain;
using GreenPoints.Services.Interfaces;
using GreenPoints.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GreenPoints.WebApi
{
    public static class DependencyInyectionConfiguration
    {
        public static IServiceCollection AddDIConfig(this IServiceCollection services)
        {
            // services
            services.AddTransient<IPremioService, PremioService>();

            // repositories
            services.AddTransient<IPremioRepository, PremioRepository>();

            return services;
        }
    }
}
