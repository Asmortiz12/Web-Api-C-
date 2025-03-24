using Microsoft.EntityFrameworkCore;
using Web_Api.Models;

namespace Web_Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Factura> Facturas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<DetalleFactura> DetalleFacturas { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración para Table-Per-Hierarchy (TPH)
        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("UserType")
            .HasValue<Cliente>("Cliente")
            .HasValue<Admin>("Admin");

        // Configuración de relaciones para Factura y DetalleFactura
        modelBuilder.Entity<Factura>()
            .HasMany(f => f.Detalles)
            .WithOne(d => d.Factura)
            .HasForeignKey(d => d.FacturaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Producto>()
            .HasMany(p => p.DetalleFacturas)
            .WithOne(d => d.Producto)
            .HasForeignKey(d => d.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relación entre Factura y Cliente
        modelBuilder.Entity<Factura>()
            .HasOne(f => f.Cliente)
            .WithMany()
            .HasForeignKey(f => f.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Datos semilla con IDs estáticos
        modelBuilder.Entity<Cliente>()
            .HasData(new Cliente {
                Id = 1,
                Name = "Juan",
                Email = "juan@example.com",
                Telefone = "1234567890",
                Password = "password123",
                DireccionEnvio = "Calle Principal 123"
            });
           
        modelBuilder.Entity<Admin>()
            .HasData(new Admin {
                Id = 2,
                Name = "Admin",
                Email = "admin@example.com",
                Telefone = "0987654321",
                Password = "admin123",
                Departamento = "IT",
                NivelAcceso = "Total"
            });

    }    // Configuración para suprimir advertencias de cambios pendientes en el modelo
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Solo para desarrollo/pruebas
            optionsBuilder.UseNpgsql("Host=localhost;Database=tu_base_de_datos;Username=tu_usuario;Password=tu_contraseña");
        }
        
        // Suprimir advertencia de cambios pendientes en el modelo
        optionsBuilder.ConfigureWarnings(warnings => 
            warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        
        base.OnConfiguring(optionsBuilder);
    }
}
