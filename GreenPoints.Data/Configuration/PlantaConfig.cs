using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenPoints.Data
{
    public class  PlantaConfig : IEntityTypeConfiguration<Planta>
    {
        public void Configure(EntityTypeBuilder<Planta> builder)
        {
            builder.ToTable("Planta", "tpf");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("planta_id");
            builder.Property(x => x.FechaCrea).HasColumnName("fecha_crea");
        }
    }
}
