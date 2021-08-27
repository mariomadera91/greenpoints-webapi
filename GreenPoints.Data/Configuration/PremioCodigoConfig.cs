using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenPoints.Data
{
    public class PremioCodigoConfig : IEntityTypeConfiguration<PremioCodigo>
    {
        public void Configure(EntityTypeBuilder<PremioCodigo> builder)
        {
            builder.ToTable("Premio_Codigo", "tpf");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("codigo_id");
            builder.Property(x => x.PremioId).HasColumnName("premio_id");
        }
    }
}
