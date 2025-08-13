using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Configurations
{
    public class Cursoconfiguracion : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso");

            builder.HasKey(e => e.IdCurso);

            builder.Property(e => e.IdCurso).HasColumnName("IdCurso");
            builder.Property(e => e.IdMateria).HasColumnName("IdMateria");
            builder.Property(e => e.Nombre).HasColumnName("Nombre");
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            builder.Property(e => e.CreatedBy).HasColumnName("created_by");
            builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            builder.Property(e => e.DeletedBy).HasColumnName("deleted_by");

            builder.HasOne(t => t.Materia).WithMany(m => m.Cursos).HasForeignKey(e => e.IdMateria);

        }
    }
}
