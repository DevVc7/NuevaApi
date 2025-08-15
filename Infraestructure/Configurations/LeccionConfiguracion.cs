using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class LeccionConfiguracion : IEntityTypeConfiguration<Leccion>
    {
        public void Configure(EntityTypeBuilder<Leccion> builder)
        {
            builder.ToTable("Leccion");

            builder.HasKey(e => e.IdLeccion);

            builder.Property(e => e.IdLeccion).HasColumnName("IdLeccion");
            builder.Property(e => e.IdCurso).HasColumnName("IdCurso");
            builder.Property(e => e.Nombre).HasColumnName("Nombre");
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            builder.Property(e => e.CreatedBy).HasColumnName("created_by");
            builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            builder.Property(e => e.DeletedBy).HasColumnName("deleted_by");

            builder.HasOne(t => t.Curso).WithMany(t => t.Leccion).HasForeignKey(t => t.IdCurso);

        }
    }
}
