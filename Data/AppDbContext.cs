using APICoreComisiones.Models;
using Microsoft.EntityFrameworkCore;

namespace APICoreComisiones.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Regla> Reglas => Set<Regla>();
        public DbSet<Vendedor> Vendedores => Set<Vendedor>();
        public DbSet<Venta> Ventas => Set<Venta>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            // Regla
            b.Entity<Regla>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Porcentaje).HasColumnType("decimal(5,4)"); 
                e.Property(x => x.MontoMinimo).HasColumnType("decimal(18,2)");
            });

            // Vendedor
            b.Entity<Vendedor>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Nombre).HasMaxLength(120).IsRequired();
                e.HasMany(x => x.Ventas)
                 .WithOne(v => v.Vendedor)
                 .HasForeignKey(v => v.VendedorId);
            });

            // Venta
            b.Entity<Venta>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Monto).HasColumnType("decimal(18,2)");
                e.HasIndex(x => new { x.VendedorId, x.FechaVenta }); 
            });
        }

    }
}
