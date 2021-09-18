using GreenPoints.Data;
using GreenPoints.Domain;
using GreenPoints.Services;
using GreenPoints.Services.Interfaces;
using GreenPoints.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GreenPoints.WebApi
{
    public static class DependencyInyectionConfiguration
    {
        public static IServiceCollection AddDIConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);

            // services
            services.AddTransient<IPremioService, PremioService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<ISocioRecicladorService, SocioRecicladorService>();
            services.AddTransient<IIntercambioService, IntercambioService>();

            // repositories
            services.AddTransient<IPremioRepository, PremioRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<ISocioRecicladorRepository, SocioRecicladorRepository>();
            services.AddTransient<IIntercambioRepository, IntercambioRepository>();

            return services;
        }
    }
}
