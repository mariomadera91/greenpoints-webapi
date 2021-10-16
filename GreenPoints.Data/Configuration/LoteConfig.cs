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
            builder.Property(x => x.FechaCreacion).HasColumnName("fecha_creacion");
            builder.Property(x => x.FechaCierre).HasColumnName("fecha_cierre").IsRequired(false);
            builder.Property(x => x.PlantaId).HasColumnName("planta_id").IsRequired(false);
        }
    }
}
