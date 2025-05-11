using Microsoft.EntityFrameworkCore;


namespace ProyectoGestionDispositivos.Models.Entidades
{
    public class LibreriaContext : DbContext
    {
        public LibreriaContext(DbContextOptions<LibreriaContext> options) : base(options)
        {
        }

        public DbSet<Marca> Marcas { get; set; }
        public DbSet<TipoMantenimiento> TipoMantenimientos { get; set; }
        public DbSet<Dispositivo> Dispositivos { get; set; }
        public DbSet<VentaServicio> VentaServicios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TipoMantenimiento>().HasIndex(c => c.NombreTipo).IsUnique();
        }
    }
}