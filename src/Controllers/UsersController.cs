using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using users_service.src.DTOs;
using users_service.src.Interface;

namespace users_service.src.Controllers
{
    /// <summary>
    /// Controlador REST responsable de exponer los endpoints
    /// para la gestión y autenticación de usuarios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Servicio de lógica de negocio para usuarios.
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor del controlador de usuarios.
        /// </summary>
        /// <param name="userService">Servicio de usuarios inyectado por dependencia.</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Crea un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="dto">DTO con los datos necesarios para crear el usuario.</param>
        /// <returns>
        /// Retorna el usuario creado con código HTTP 201,
        /// o un error 400 en caso de validación fallida.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { message = "El cuerpo de la solicitud no puede estar vacío." });
            }

            try
            {
                var createdUserDto = await _userService.CreateUserAsync(dto);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUserDto.Id }, createdUserDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene la lista de todos los usuarios registrados.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsersAsync();
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un usuario según su identificador único.
        /// </summary>
        /// <param name="id">Identificador GUID del usuario.</param>
        /// <returns>
        /// Retorna el usuario encontrado o un error 404 si no existe.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { message = "El ID del usuario no puede estar vacío." });
            }

            try
            {
                var userDto = await _userService.GetUserByIdAsync(id);
                return Ok(userDto);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        /// <param name="id">Identificador del usuario.</param>
        /// <param name="dto">DTO con los nuevos datos del usuario.</param>
        /// <returns>
        /// Retorna el usuario actualizado o un error 400 en caso de fallo.
        /// </returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { message = "El cuerpo de la solicitud no puede estar vacío." });
            }

            if (id == Guid.Empty)
            {
                return BadRequest(new { message = "El ID del usuario no puede estar vacío." });
            }

            try
            {
                var updatedUserDto = await _userService.UpdateUserAsync(id, dto);
                return Ok(updatedUserDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina lógicamente un usuario del sistema.
        /// </summary>
        /// <param name="id">Identificador del usuario.</param>
        /// <returns>
        /// Código HTTP 204 si la eliminación fue exitosa,
        /// o 404 si el usuario no existe.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { message = "El ID del usuario no puede estar vacío." });
            }

            var success = await _userService.DeleteUserAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Autentica un usuario mediante nombre de usuario o correo electrónico
        /// y contraseña.
        /// </summary>
        /// <param name="dto">DTO con las credenciales de autenticación.</param>
        /// <returns>
        /// Retorna el usuario autenticado o un error 401 si las credenciales son incorrectas.
        /// </returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Identifier) || string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest(new { message = "Falta el identificador o la contraseña." });
            }

            try
            {
                var userDto = await _userService.AuthenticateUserAsync(dto);
                return Ok(userDto);
            }
            catch
            {
                return Unauthorized(new { message = "Usuario/Email o contraseña incorrectos." });
            }
        }
    }
}
