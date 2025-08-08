using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    internal class RolUserConfiguration : IEntityTypeConfiguration<RolUser>
    {
        public void Configure(EntityTypeBuilder<RolUser> builder)
        {
            builder.ToTable("UsuarioRol");

            builder.HasKey(e => e.IdUsuarioRol);

            builder.Property(e => e.IdUsuarioRol).HasColumnName("IdUsuarioRol");
            builder.Property(e => e.IdUsuario).HasColumnName("IdUsuario");
            builder.Property(e => e.IdRol).HasColumnName("IdRol");
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            builder.Property(e => e.CreatedBy).HasColumnName("created_by");
            builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            builder.Property(e => e.DeletedBy).HasColumnName("deleted_by");

            builder.HasOne(e => e.Rol).WithMany().HasForeignKey(e => e.IdRol);
            builder.HasOne(e => e.Usuario).WithMany().HasForeignKey(e => e.IdUsuario);

        }
    }
}
