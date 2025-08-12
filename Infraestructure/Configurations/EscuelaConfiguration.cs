using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class EscuelaConfiguration : IEntityTypeConfiguration<Escuela>
    {
        public void Configure(EntityTypeBuilder<Escuela> builder)
        {
            builder.ToTable("Escuela");

            builder.HasKey(e => e.IdEscuela);

            builder.Property(e => e.IdEscuela).HasColumnName("IdEscuela");
            builder.Property(e => e.Nombre).HasColumnName("Nombre");
            builder.Property(e => e.Telefono).HasColumnName("Telefono");
            builder.Property(e => e.Codigo).HasColumnName("Codigo");
            builder.Property(e => e.Direccion).HasColumnName("Direccion");
            builder.Property(e => e.Correo).HasColumnName("Correo");
            builder.Property(e => e.Descripcion).HasColumnName("Descripcion");

        }
    }
}
