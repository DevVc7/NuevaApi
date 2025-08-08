using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Estudiante>
    {
        public void Configure(EntityTypeBuilder<Estudiante> builder)
        {
            builder.ToTable("EstudianteInfo");

            builder.HasKey(e => e.IdEstudiante);

            builder.Property(e => e.IdEstudiante).HasColumnName("IdEstudiante");
            builder.Property(e => e.IdUsuario).HasColumnName("IdUsuario");
            builder.Property(e => e.CodEstudiante).HasColumnName("CodEstudiante");
            builder.Property(e => e.IdGrado).HasColumnName("IdGrado");
            builder.Property(e => e.IdSeccion).HasColumnName("IdSeccion");
            builder.Property(e => e.Descripcion).HasColumnName("Descripcion");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            builder.Property(e => e.CreatedBy).HasColumnName("created_by");
            builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            builder.Property(e => e.DeletedBy).HasColumnName("deleted_by");

            builder.HasOne(e => e.Usuario).WithMany().HasForeignKey(e => e.IdUsuario);
            builder.HasOne(e => e.Grado).WithMany().HasForeignKey(e => e.IdGrado);
        }
    }
}