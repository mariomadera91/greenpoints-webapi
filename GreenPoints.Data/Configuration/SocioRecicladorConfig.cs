using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenPoints.Data
{
    public class SocioRecicladorConfig : IEntityTypeConfiguration<SocioReciclador>
    {
        public void Configure(EntityTypeBuilder<SocioReciclador> builder)
        {
            builder.ToTable("Socio_Reciclador", "tpf");
            builder.Property(x => x.Id).HasColumnName("socio_id");
            builder.Property(x => x.FechaNac).HasColumnName("fecha_nac");
            builder.Property(x => x.UsuarioId).HasColumnName("usuario_id");
        }
    }
}
