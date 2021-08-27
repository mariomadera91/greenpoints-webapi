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
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("punto_id");
        }
    }
}
