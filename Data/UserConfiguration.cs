using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_Api.Models;

namespace Web_Api.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Configuración de la tabla
            builder.ToTable("users");
            
            // Clave primaria
            builder.HasKey(u => u.Id);
            
            // Discriminador para TPH (Table-Per-Hierarchy)
            builder.HasDiscriminator<string>("UserType")
                .HasValue<Cliente>("Cliente")
                .HasValue<Admin>("Admin");
            
            // Propiedades
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);
                
            builder.Property(u => u.Telefone)
                .HasMaxLength(20);
                
            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);
                
            // Índice para búsquedas rápidas por email
            builder.HasIndex(u => u.Email)
                .IsUnique();
                
            // Datos semilla
            builder.HasData(
                new Cliente 
                { 
                    Id = 1, 
                    Name = "Juan", 
                    Email = "juan@example.com", 
                    Telefone = "1234567890", 
                    Password = "password123",
                    DireccionEnvio = "Calle Principal 123"
                },
                new Admin 
                { 
                    Id = 2, 
                    Name = "Admin", 
                    Email = "admin@example.com", 
                    Telefone = "0987654321", 
                    Password = "admin123",
                    Departamento = "IT",
                    NivelAcceso = "Total"
                }
            );
        }
    }
}