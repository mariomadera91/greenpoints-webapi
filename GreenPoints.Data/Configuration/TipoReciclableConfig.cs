using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenPoints.Data
{
    public class TipoReciclableConfig : IEntityTypeConfiguration<TipoReciclable>
    {
        public void Configure(EntityTypeBuilder<TipoReciclable> builder)
        {
            builder.ToTable("Tipo_Reciclable", "tpf");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("tipo_id");
            builder.Property(x => x.PuntosKg).HasColumnName("puntos_kg");
        }
    }
}
