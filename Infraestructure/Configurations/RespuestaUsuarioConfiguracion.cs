using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class RespuestaUsuarioConfiguracion : IEntityTypeConfiguration<RespuestaUsuario>
    {
        public void Configure(EntityTypeBuilder<RespuestaUsuario> builder)
        {
            builder.ToTable("RespuestaUsuario");

            builder.HasKey(e => e.IdRespuesta);

            builder.Property(e => e.IdRespuesta).HasColumnName("IdRespuesta");
            builder.Property(e => e.IdUsuario).HasColumnName("IdUsuario");
            builder.Property(e => e.IdPregunta).HasColumnName("IdPregunta");
            builder.Property(e => e.IdOpcion).HasColumnName("IdOpcion");
            builder.Property(e => e.FechaRespuesta).HasColumnName("FechaRespuesta");
            builder.Property(e => e.TiempoRespuesta).HasColumnName("TiempoRespuesta");
            builder.Property(e => e.PuntajeObtenido).HasColumnName("PuntajeObtenido").HasPrecision(10, 2);

            builder.HasOne(e => e.OpcionesRpt).WithMany().HasForeignKey(e => e.IdOpcion);
            builder.HasOne(e => e.Pregunta).WithMany().HasForeignKey(e => e.IdPregunta);


        }
    }
}
