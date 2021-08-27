using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenPoints.Data
{
    public class LoteConfig : IEntityTypeConfiguration<Lote>
    {
        public void Configure(EntityTypeBuilder<Lote> builder)
        {
            builder.ToTable("Lote", "tpf");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("lote_id");
            builder.Property(x => x.PuntoId).HasColumnName("punto_id");
            builder.Property(x => x.TipoId).HasColumnName("tipo_id");
            builder.Property(x => x.FechaCrea).HasColumnName("fecha_crea");
            builder.Property(x => x.PlantaId).HasColumnName("planta_id");
        }
    }
}
