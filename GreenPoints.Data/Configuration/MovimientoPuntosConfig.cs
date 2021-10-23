using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenPoints.Data
{
    public class MovimientoPuntosConfig : IEntityTypeConfiguration<MovimientoPuntos>
    {
        public void Configure(EntityTypeBuilder<MovimientoPuntos> builder)
        {
            builder.ToTable("Movimiento_puntos", "tpf");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("mov_id");
            builder.Property(x => x.SocioId).HasColumnName("socio_id");
        }
    }
}
