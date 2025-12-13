using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using users_service.src.DTOs;
using users_service.src.Interface;
using users_service.src.Mappers;
using users_service.src.models;

namespace users_service.src.Services
{
    /// <summary>
    /// Servicio de lógica de negocio encargado de la gestión de usuarios.
    /// Implementa la interfaz IUserService y opera sobre una colección
    /// de usuarios almacenada en memoria.
    /// </summary>
    /// <remarks>
    /// Este servicio utiliza almacenamiento en memoria mediante una lista,
    /// por lo que los datos se pierden al reiniciar la aplicación.
    /// Está diseñado para fines académicos y pruebas.
    /// </remarks>
    public class UserService : IUserService
    {
        /// <summary>
        /// Lista en memoria que almacena los usuarios registrados.
        /// </summary>
        private readonly List<User> _users = new();

        /// <summary>
        /// Constructor del servicio de usuarios.
        /// Inicializa la colección y carga datos de ejemplo (seed).
        /// </summary>
        public UserService()
        {
            SeedInitialData();
        }

        /// <summary>
        /// Carga datos iniciales de usuarios en memoria.
        /// Se utiliza para pruebas y demostraciones del sistema.
        /// </summary>
        private void SeedInitialData()
        {
            _users.Add(new User
            {
                Id = Guid.NewGuid(),
                FullName = "Administrador InsightFlow",
                Email = "admin@insightflow.com",
                Username = "admin",
                Password = "password123",
                DateOfBirth = new DateOnly(1990, 1, 1),
                Address = "Sede Central, Santiago",
                PhoneNumber = "987654321",
                UserStatus = true
            });

            _users.Add(new User
            {
                Id = Guid.NewGuid(),
                FullName = "Jose Acuña",
                Email = "j.acuna@ucn.cl",
                Username = "j_acuna",
                Password = "test",
                DateOfBirth = new DateOnly(2003, 5, 10),
                Address = "Antofagasta, Chile",
                PhoneNumber = "912345678",
                UserStatus = true
            });

            _users.Add(new User
            {
                Id = Guid.NewGuid(),
                FullName = "Neymar Junior",
                Email = "NeyNey@test.com",
                Username = "Santos10",
                Password = "futbol",
                DateOfBirth = new DateOnly(1980, 12, 12),
                Address = "Santos, Brasil",
                PhoneNumber = "900000000",
                UserStatus = true
            });
        }

        /// <summary>
        /// Crea un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="userCreateDto">DTO con los datos de creación del usuario.</param>
        /// <returns>DTO con la información del usuario creado.</returns>
        /// <exception cref="Exception">
        /// Se lanza si el correo electrónico o nombre de usuario ya existen.
        /// </exception>
        public Task<UserResponseDto> CreateUserAsync(UserCreateDto userCreateDto)
        {
            var newUser = UserMapper.ToEntity(userCreateDto);

            if (_users.Any(u => u.Email == newUser.Email))
                throw new Exception("El correo electrónico ya existe.");

            if (_users.Any(u => u.Username == newUser.Username))
                throw new Exception("Nombre de usuario ya existe.");

            newUser.Id = Guid.NewGuid();
            newUser.UserStatus = true;

            _users.Add(newUser);

            return Task.FromResult(UserMapper.ToResponseDto(newUser));
        }

        /// <summary>
        /// Obtiene un usuario según su identificador único.
        /// </summary>
        /// <param name="userId">Identificador GUID del usuario.</param>
        /// <returns>DTO con los datos del usuario encontrado.</returns>
        /// <exception cref="Exception">
        /// Se lanza si el usuario no existe.
        /// </exception>
        public Task<UserResponseDto> GetUserByIdAsync(Guid userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
                throw new Exception("Usuario no encontrado o inactivo.");

            return Task.FromResult(UserMapper.ToResponseDto(user));
        }

        /// <summary>
        /// Actualiza los datos básicos de un usuario existente.
        /// </summary>
        /// <param name="userId">Identificador del usuario a actualizar.</param>
        /// <param name="userUpdateDto">DTO con los nuevos datos del usuario.</param>
        /// <returns>DTO con la información actualizada del usuario.</returns>
        /// <exception cref="Exception">
        /// Se lanza si el usuario no existe o el nombre de usuario ya está en uso.
        /// </exception>
        public Task<UserResponseDto> UpdateUserAsync(Guid userId, UserUpdateDto userUpdateDto)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId && u.UserStatus);

            if (user == null)
                throw new Exception("Usuario no encontrado.");

            if (_users.Any(u => u.Username == userUpdateDto.Username && u.Id != userId))
                throw new Exception("El nombre de usuario ya está en uso por otro cliente.");

            user.FullName = userUpdateDto.FullName;
            user.Username = userUpdateDto.Username;

            return Task.FromResult(UserMapper.ToResponseDto(user));
        }

        /// <summary>
        /// Elimina lógicamente un usuario del sistema.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        /// <returns>
        /// True si el usuario fue eliminado correctamente, false si no fue encontrado.
        /// </returns>
        public Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId && u.UserStatus);

            if (user == null)
                return Task.FromResult(false);

            user.UserStatus = false;
            return Task.FromResult(true);
        }

        /// <summary>
        /// Obtiene la lista de todos los usuarios registrados.
        /// </summary>
        /// <returns>Colección de usuarios en formato DTO.</returns>
        public Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = _users.Select(u => UserMapper.ToResponseDto(u));
            return Task.FromResult(users);
        }

        /// <summary>
        /// Autentica un usuario mediante nombre de usuario o correo electrónico
        /// y contraseña.
        /// </summary>
        /// <param name="dto">DTO con credenciales de autenticación.</param>
        /// <returns>DTO del usuario autenticado.</returns>
        /// <exception cref="Exception">
        /// Se lanza si las credenciales son incorrectas.
        /// </exception>
        public Task<UserResponseDto> AuthenticateUserAsync(LoginUserDto dto)
        {
            var user = _users.FirstOrDefault(u =>
                (u.Username.Equals(dto.Identifier, StringComparison.OrdinalIgnoreCase) ||
                 u.Email.Equals(dto.Identifier, StringComparison.OrdinalIgnoreCase)) &&
                u.UserStatus);

            if (user == null || user.Password != dto.Password)
                throw new Exception("Credenciales incorrectas.");

            return Task.FromResult(UserMapper.ToResponseDto(user));
        }
    }
}
