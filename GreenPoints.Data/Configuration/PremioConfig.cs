using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenPoints.Data
{
    public class PremioConfig : IEntityTypeConfiguration<Premio>
    {
        public void Configure(EntityTypeBuilder<Premio> builder)
        {
            builder.ToTable("Premio", "tpf");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("premio_id");
            builder.Property(x => x.VigenciaDesde).HasColumnName("vigencia_desde");
            builder.Property(x => x.VigenciaHasta).HasColumnName("vigencia_hasta");
            builder.Property(x => x.SponsorId).HasColumnName("sponsor_id");
        }
    }
}
