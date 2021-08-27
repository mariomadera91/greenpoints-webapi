using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenPoints.Data
{
    public class PuntoReciclajeTipoReciclableConfig : IEntityTypeConfiguration<PuntoReciclajeTipoReciclable>
    {
        public void Configure(EntityTypeBuilder<PuntoReciclajeTipoReciclable> builder)
        {
            builder.ToTable("PuntoReciclaje_TipoReciclable", "tpf");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("punto_tipo_id");
            builder.Property(x => x.PuntoId).HasColumnName("punto_id");
            builder.Property(x => x.TipoId).HasColumnName("tipo_id");
        }
    }
}
