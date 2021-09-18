using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenPoints.Data
{
    public class PuntoReciclajeConfig : IEntityTypeConfiguration<PuntoReciclaje>
    {
        public void Configure(EntityTypeBuilder<PuntoReciclaje> builder)
        {
            builder.ToTable("Punto_Reciclaje", "tpf");
            builder.Property(x => x.Id).HasColumnName("punto_id");
            builder.Property(x => x.UsuarioId).HasColumnName("usuario_id");
        }
    }
}
