using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class PreguntaConfiguration : IEntityTypeConfiguration<Pregunta>
    {
        public void Configure(EntityTypeBuilder<Pregunta> builder)
        {
            builder.ToTable("Pregunta");

            builder.HasKey(e => e.IdPregunta);

            builder.Property(e => e.IdPregunta).HasColumnName("IdPregunta");
            builder.Property(e => e.Dificultad).HasColumnName("Dificultad");
            builder.Property(e => e.Enunciado).HasColumnName("Enunciado");
            builder.Property(e => e.Explicacion).HasColumnName("Explicacion");
            builder.Property(e => e.Pista).HasColumnName("Pista");
            builder.Property(e => e.Puntaje).HasColumnName("Puntaje").HasPrecision(10, 2);
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.IdMateria).HasColumnName("IdMateria");
            builder.Property(e => e.idCurso).HasColumnName("idCurso");
            builder.Property(e => e.IdLeccion).HasColumnName("IdLeccion");
            builder.Property(e => e.IdGrado).HasColumnName("IdGrado");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            builder.Property(e => e.CreatedBy).HasColumnName("created_by");
            builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            builder.Property(e => e.DeletedBy).HasColumnName("deleted_by");


            builder.HasOne(e => e.Grado).WithMany().HasForeignKey(e => e.IdGrado);
            builder.HasOne(e => e.Materia).WithMany().HasForeignKey(e => e.IdMateria);
        }
    }
}
