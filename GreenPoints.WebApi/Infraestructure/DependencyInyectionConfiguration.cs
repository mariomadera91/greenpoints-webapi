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
            services.AddTransient<IPuntoReciclajeService, PuntoReciclajeService>();
            services.AddTransient<IIntercambioService, IntercambioService>();
            services.AddTransient<ITipoReciclableService, TipoReciclableService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<ILoteService, LoteService>();

            // repositories
            services.AddTransient<IPremioRepository, PremioRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<ISocioRecicladorRepository, SocioRecicladorRepository>();
            services.AddTransient<IPuntoReciclajeRepository, PuntoReciclajeRepository>();
            services.AddTransient<IIntercambioRepository, IntercambioRepository>();
            services.AddTransient<ITipoReciclableRepository, TipoReciclableRepository>();
            services.AddTransient<ILoteRepository, LoteRepository>();

            return services;
        }
    }
}
