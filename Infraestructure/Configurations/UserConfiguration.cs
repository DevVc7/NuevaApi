using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(e => e.IdUsuario);

            builder.Property(e => e.IdUsuario).HasColumnName("IdUsuario");
            builder.Property(e => e.NombreCompleto).HasColumnName("NombreCompleto");
            builder.Property(e => e.Nombres).HasColumnName("Nombres");
            builder.Property(e => e.Apellidos).HasColumnName("Apellidos");
            builder.Property(e => e.Correo).HasColumnName("Correo");
            builder.Property(e => e.Password).HasColumnName("Password");
            builder.Property(e => e.NroDocumento).HasColumnName("NroDocumento");
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.Biografia).HasColumnName("Biografia");
            builder.Property(e => e.Photo).HasColumnName("Photo");
            builder.Property(e => e.Photo).HasColumnName("Photo");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            builder.Property(e => e.CreatedBy).HasColumnName("created_by");
            builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            builder.Property(e => e.DeletedBy).HasColumnName("deleted_by");

            builder.HasOne(t => t.Escuela).WithMany().HasForeignKey(t => t.IdEscuela);


        }
    }
}
