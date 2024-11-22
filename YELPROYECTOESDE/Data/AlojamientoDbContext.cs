
using Microsoft.EntityFrameworkCore; 
using YELPROYECTOESDE.Models;

namespace YELPROYECTOESDE.Data
{
    public class AlojamientoDbContext : DbContext
    {
        public AlojamientoDbContext(DbContextOptions<AlojamientoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Alojamiento> Alojamientos { get; set; }
        public DbSet<Comodidad> Comodidades { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<DetalleAlojamientoComodidad> DetallesAlojamientoComodidad { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de relaciones
            modelBuilder.Entity<DetalleAlojamientoComodidad>()
                .HasOne(d => d.Alojamiento)
                .WithMany(a => a.DetallesAlojamientoComodidad)
                .HasForeignKey(d => d.IdAlojamiento);

            modelBuilder.Entity<DetalleAlojamientoComodidad>()
                .HasOne(d => d.Comodidad)
                .WithMany(c => c.DetallesAlojamientoComodidad)
                .HasForeignKey(d => d.ComodidadId);

            modelBuilder.Entity<Alojamiento>()
                .HasOne(a => a.Tipo)
                .WithMany(t => t.Alojamientos)
                .HasForeignKey(a => a.TipoId);
        }
    }
}
