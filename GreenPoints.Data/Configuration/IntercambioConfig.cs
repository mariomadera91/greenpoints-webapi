using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenPoints.Data
{
    public class IntercambioConfig : IEntityTypeConfiguration<Intercambio>
    {
        public void Configure(EntityTypeBuilder<Intercambio> builder)
        {
            builder.ToTable("Intercambio", "tpf");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("inter_id");
            builder.Property(x => x.SocioId).HasColumnName("socio_id");
            builder.Property(x => x.PuntoId).HasColumnName("punto_id");
            builder.Property(x => x.Puntos).HasColumnName("puntos");
        }
    }
}
