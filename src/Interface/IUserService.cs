using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using users_service.src.DTOs;

namespace users_service.src.Interface
{
    /// <summary>
    /// Define el contrato de servicios para la gestión de usuarios.
    /// Expone las operaciones de creación, consulta, actualización,
    /// eliminación lógica y autenticación.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Crea un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="userCreateDto">DTO con los datos necesarios para crear el usuario.</param>
        /// <returns>DTO con la información del usuario creado.</returns>
        Task<UserResponseDto> CreateUserAsync(UserCreateDto userCreateDto);

        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        /// <param name="userId">Identificador único del usuario.</param>
        /// <param name="userUpdateDto">DTO con los nuevos datos del usuario.</param>
        /// <returns>DTO con la información actualizada del usuario.</returns>
        Task<UserResponseDto> UpdateUserAsync(Guid userId, UserUpdateDto userUpdateDto);

        /// <summary>
        /// Obtiene un usuario según su identificador único.
        /// </summary>
        /// <param name="userId">Identificador GUID del usuario.</param>
        /// <returns>DTO con los datos del usuario.</returns>
        Task<UserResponseDto> GetUserByIdAsync(Guid userId);

        /// <summary>
        /// Obtiene la lista de todos los usuarios registrados en el sistema.
        /// </summary>
        /// <returns>Colección de usuarios en formato DTO.</returns>
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();

        /// <summary>
        /// Elimina lógicamente un usuario del sistema.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        /// <returns>
        /// True si el usuario fue eliminado correctamente; false en caso contrario.
        /// </returns>
        Task<bool> DeleteUserAsync(Guid userId);

        /// <summary>
        /// Autentica un usuario mediante nombre de usuario o correo electrónico
        /// y contraseña.
        /// </summary>
        /// <param name="dto">DTO con las credenciales de autenticación.</param>
        /// <returns>DTO del usuario autenticado.</returns>
        Task<UserResponseDto> AuthenticateUserAsync(LoginUserDto dto);
    }
}
