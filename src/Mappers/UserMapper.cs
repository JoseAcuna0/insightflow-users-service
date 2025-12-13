using users_service.src.DTOs;
using users_service.src.models;

namespace users_service.src.Mappers
{
    /// <summary>
    /// Clase responsable de la transformación de datos entre
    /// entidades de dominio y Data Transfer Objects (DTOs)
    /// relacionados con usuarios.
    /// </summary>
    public class UserMapper
    {
        /// <summary>
        /// Convierte una entidad <see cref="User"/> en un
        /// <see cref="UserResponseDto"/> para ser enviado como respuesta.
        /// </summary>
        /// <param name="user">Entidad usuario del dominio.</param>
        /// <returns>
        /// DTO de respuesta con los datos públicos del usuario.
        /// </returns>
        public static UserResponseDto ToResponseDto(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Username = user.Username,
                UserStatus = user.UserStatus,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address
            };
        }

        /// <summary>
        /// Convierte un <see cref="UserCreateDto"/> en una entidad
        /// <see cref="User"/> del dominio.
        /// </summary>
        /// <param name="dto">DTO con los datos necesarios para crear un usuario.</param>
        /// <returns>
        /// Entidad <see cref="User"/> lista para ser persistida.
        /// </returns>
        public static User ToEntity(UserCreateDto dto)
        {
            return new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Username = dto.Username,
                Password = dto.Password,
                DateOfBirth = dto.DateOfBirth,
                Address = dto.Address
            };
        }
    }
}
