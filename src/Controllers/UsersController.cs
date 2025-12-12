using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using users_service.src.DTOs;
using users_service.src.Interface;
using users_service.src.Services;

namespace users_service.src.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


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

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsersAsync();

            return Ok(response);
        }

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

        [HttpPost("login")] 
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto) // Usa el DTO CORREGIDO
        {
            if (dto == null || string.IsNullOrEmpty(dto.Identifier) || string.IsNullOrEmpty(dto.Password))
            {
                // Se añade un manejo de error temprano si la deserialización falla y los campos son nulos
                return BadRequest(new { message = "Falta el identificador o la contraseña." });
            }
            
            try
            {
                // Llama al servicio con el DTO
                var userDto = await _userService.AuthenticateUserAsync(dto); 
                return Ok(userDto); 
            }
            catch (Exception ex)
            {
                // Esto es lo que devuelve el error de credenciales
                return Unauthorized(new { message = "Usuario/Email o contraseña incorrectos." });
            }
        }


    }
}