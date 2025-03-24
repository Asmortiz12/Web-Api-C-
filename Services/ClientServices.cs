using Microsoft.EntityFrameworkCore;
using Web_Api.Data;
using Web_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api.Services
{
    public interface IClientService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(int id, User user);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> UserExistsAsync(int id);
        Task<bool> EmailExistsAsync(string email);
    }

    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ClientService> _logger;

        public ClientService(ApplicationDbContext context, ILogger<ClientService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            // Validar que el Role sea un valor válido del enum
            if (!Enum.IsDefined(typeof(UserRole), user.Role))
            {
                throw new ArgumentException($"El valor de 'role' debe ser 0 (Cliente) o 1 (Admin). Valor recibido: {(int)user.Role}");
            }

            // Verificar si ya existe un usuario con el mismo email
            if (await EmailExistsAsync(user.Email))
            {
                throw new InvalidOperationException($"Ya existe un usuario con el email {user.Email}");
            }

            // Asignar UserType basado en el enum Role
            if (user.Role == UserRole.Admin)
            {
                // Crear un Admin
                var admin = new Admin
                {
                    Name = user.Name,
                    Email = user.Email,
                    Telefone = user.Telefone,
                    Password = user.Password,
                    Role = UserRole.Admin,
                   
                    Departamento = "IT",
                    NivelAcceso = "Total"
                };
                
                _context.Admins.Add(admin);
                await _context.SaveChangesAsync();
                
                return admin;
            }
            else // UserRole.Cliente
            {
                // Crear un Cliente
                var cliente = new Cliente
                {
                    Name = user.Name,
                    Email = user.Email,
                    Telefone = user.Telefone,
                    Password = user.Password,
                    Role = UserRole.Cliente,
                 
                    DireccionEnvio = "Dirección pendiente"
                };
                
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                
                return cliente;
            }
        }

        public async Task<bool> UpdateUserAsync(int id, User user)
        {
            // Validar que el Role sea un valor válido del enum
            if (!Enum.IsDefined(typeof(UserRole), user.Role))
            {
                throw new ArgumentException($"El valor de 'role' debe ser 0 (Cliente) o 1 (Admin). Valor recibido: {(int)user.Role}");
            }

            // Obtener el usuario existente
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return false;
            }

            // Si el rol ha cambiado, necesitamos manejar esto de manera especial
            if (existingUser.Role != user.Role)
            {
                // Eliminar el usuario existente
                _context.Users.Remove(existingUser);
                await _context.SaveChangesAsync();

                // Crear un nuevo usuario del tipo correcto
                if (user.Role == UserRole.Admin)
                {
                    var admin = new Admin
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Telefone = user.Telefone,
                        Password = user.Password,
                        Role = UserRole.Admin,
                        
                        Departamento = "IT",
                        NivelAcceso = "Total"
                    };
                    
                    _context.Admins.Add(admin);
                }
                else // UserRole.Cliente
                {
                    var cliente = new Cliente
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Telefone = user.Telefone,
                        Password = user.Password,
                        Role = UserRole.Cliente,
                        
                        DireccionEnvio = "Dirección pendiente"
                    };
                    
                    _context.Clientes.Add(cliente);
                }
            }
            else
            {
                // Actualizar propiedades
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Telefone = user.Telefone;
                existingUser.Password = user.Password;
                existingUser.Role = user.Role;
                // Role ya está verificado y no ha cambiado
                
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}