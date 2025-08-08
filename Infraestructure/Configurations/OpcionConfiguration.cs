using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class OpcionConfiguration : IEntityTypeConfiguration<OpcionesRpt>
    {
        public void Configure(EntityTypeBuilder<OpcionesRpt> builder)
        {
            builder.ToTable("OpcionesRpta");

            builder.HasKey(e => e.IdOpcion);

            builder.Property(e => e.IdOpcion).HasColumnName("IdOpcion");
            builder.Property(e => e.IdPregunta).HasColumnName("IdPregunta");
            builder.Property(e => e.Texto).HasColumnName("Texto");
            builder.Property(e => e.EsCorrecta).HasColumnName("EsCorrecta");

            builder.HasOne(e => e.Pregunta).WithMany(t => t.OpcionesRpt).HasForeignKey(e => e.IdPregunta);

        }
    }
}
