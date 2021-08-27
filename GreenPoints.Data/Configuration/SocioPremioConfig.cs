using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenPoints.Data
{
    public class  SocioPremioConfig : IEntityTypeConfiguration<SocioPremio>
    {
        public void Configure(EntityTypeBuilder<SocioPremio> builder)
        {
            builder.ToTable("Socio_Premio", "tpf");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("canje_id");
            builder.Property(x => x.SocioId).HasColumnName("socio_id");
            builder.Property(x => x.PremioId).HasColumnName("premio_id");
            builder.Property(x => x.CodigoId).HasColumnName("codigo_id");
        }
    }
}
