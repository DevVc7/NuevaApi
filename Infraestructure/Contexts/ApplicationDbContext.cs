using Domain.View;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace Infraestructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<ReporteUsuarioDto> ReporteUsuarios { get; set; }
        public DbSet<ReporteUsuario> Reporte { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ReporteUsuarioDto>().HasNoKey().Property(r => r.TotalPuntaje).HasPrecision(10, 2);
            modelBuilder.Entity<ReporteUsuario>().HasNoKey().Property(r => r.PuntajeObtenido).HasPrecision(10, 2); ;
        }
    }
}
