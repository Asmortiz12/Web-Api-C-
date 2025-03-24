using Microsoft.AspNetCore.Mvc;
using Web_Api.Models;
using Web_Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService clientService, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        // GET: api/Client
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetClients()
        {
            try
            {
                var users = await _clientService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuarios");
                return StatusCode(500, "Error interno del servidor al obtener usuarios");
            }
        }

        // GET: api/Client/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetClient(int id)
        {
            try
            {
                var user = await _clientService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound($"No existe un usuario con el Id {id}");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuario {Id}", id);
                return StatusCode(500, $"Error interno del servidor al obtener usuario {id}");
            }
        }

        // POST: api/Client
        [HttpPost]
        public async Task<ActionResult<User>> CreateClient(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdUser = await _clientService.CreateUserAsync(user);
                return CreatedAtAction(nameof(GetClient), new { id = createdUser.Id }, createdUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear usuario");
                return StatusCode(500, "Error interno del servidor al crear usuario");
            }
        }

        // PUT: api/Client/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID en el cuerpo de la solicitud");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _clientService.UpdateUserAsync(id, user);
                if (!result)
                {
                    return NotFound($"No existe un usuario con el Id {id}");
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar usuario {Id}", id);
                return StatusCode(500, $"Error interno del servidor al actualizar usuario {id}");
            }
        }

        // DELETE: api/Client/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                var result = await _clientService.DeleteUserAsync(id);
                if (!result)
                {
                    return NotFound($"No existe un usuario con el Id {id}");
                }
                return Ok($"Usuario con Id {id} eliminado correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar usuario {Id}", id);
                return StatusCode(500, $"Error interno del servidor al eliminar usuario {id}");
            }
        }
    }
}
