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

            //builder.Ignore(x => x.UserName);
            //builder.Ignore(x => x.Password);
            //builder.Ignore(x => x.Rol);
            //builder.Ignore(x => x.Imagen);
            //builder.Ignore(x => x.Activo);
        }
    }
}
