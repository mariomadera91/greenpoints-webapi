using GreenPoints.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenPoints.Data
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario", "tpf");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("usuario_id");
            builder.Property(x => x.UserName).HasColumnName("usuario");
            builder.Property(x => x.LastPasswordReset).HasColumnName("last_password_reset");
        }
    }
}
