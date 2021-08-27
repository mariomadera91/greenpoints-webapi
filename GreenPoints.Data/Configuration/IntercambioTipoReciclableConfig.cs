using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenPoints.Data
{
    public class IntercambioTipoReciclableConfig : IEntityTypeConfiguration<IntercambioTipoReciclable>
    {
        public void Configure(EntityTypeBuilder<IntercambioTipoReciclable> builder)
        {
            builder.ToTable("Intercambio_TipoRec", "tpf");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("inter_tipo_id");
            builder.Property(x => x.IntercambioId).HasColumnName("inter_id");
            builder.Property(x => x.TipoId).HasColumnName("tipo_id");
            builder.Property(x => x.LoteId).HasColumnName("lote_id");
        }
    }
}
