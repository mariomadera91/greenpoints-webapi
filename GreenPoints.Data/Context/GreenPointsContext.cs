using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GreenPoints.Data
{
    public class GreenPointsContext : DbContext
    {
        public GreenPointsContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("GreenPoints");
            optionsBuilder.UseMySQL(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfiguracionConfig());
            modelBuilder.ApplyConfiguration(new IntercambioConfig());
            modelBuilder.ApplyConfiguration(new IntercambioTipoReciclableConfig());
            modelBuilder.ApplyConfiguration(new LoteConfig());
            modelBuilder.ApplyConfiguration(new PlantaConfig());
            modelBuilder.ApplyConfiguration(new PremioCodigoConfig());
            modelBuilder.ApplyConfiguration(new PremioConfig());
            modelBuilder.ApplyConfiguration(new PuntoReciclajeConfig());
            modelBuilder.ApplyConfiguration(new PuntoReciclajeTipoReciclableConfig());
            modelBuilder.ApplyConfiguration(new SocioPremioConfig());
            modelBuilder.ApplyConfiguration(new SocioRecicladorConfig());
            modelBuilder.ApplyConfiguration(new SponsorConfig());
            modelBuilder.ApplyConfiguration(new TipoReciclableConfig());
        }

        public DbSet<Configuracion> Configuraciones { get; set; }
        public DbSet<Intercambio> Intercambios { get; set; }
        public DbSet<IntercambioTipoReciclable> IntercambiosTiposReciclables { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Planta> Plantas { get; set; }
        public DbSet<Premio> Premios { get; set; }
        public DbSet<PremioCodigo> PremiosCodigos { get; set; }
        public DbSet<PuntoReciclaje> PuntosReciclaje { get; set; }
        public DbSet<PuntoReciclajeTipoReciclable> PuntosReciclajeTiposReciclables { get; set; }
        public DbSet<SocioPremio> SociosPremios { get; set; }
        public DbSet<SocioReciclador> SociosRecicladores { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<TipoReciclable> TiposReciclables { get; set; }
    }
}
