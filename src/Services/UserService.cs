
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
    public class UserService : IUserService
    {
    

        private readonly List<User> _users = new();

        public UserService()
        {
            _users = new List<User>();
            SeedInitialData();
        }

        private void SeedInitialData()
        {
            // --- USUARIOS DEL SEEDER ---
            
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

        public Task<UserResponseDto> CreateUserAsync(UserCreateDto userCreateDto)
        {
            var newUser = UserMapper.ToEntity(userCreateDto);

            if (_users.Any(u => u.Email == newUser.Email))
            {
                throw new Exception("El correo electrónico ya existe.");
            }

            if (_users.Any(u => u.Username == newUser.Username))
            {
                throw new Exception("Nombre de usuario ya existe.");
            }
            
            newUser.Id = Guid.NewGuid();
            newUser.UserStatus = true;

            _users.Add(newUser);

            var userResponseDto = UserMapper.ToResponseDto(newUser);

            return Task.FromResult(userResponseDto);
        }

        public Task<UserResponseDto> GetUserByIdAsync(Guid userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            
            if (user == null)
            {
                throw new Exception("Usuario no encontrado o inactivo."); 
            }

            var responseDto = UserMapper.ToResponseDto(user);
            return Task.FromResult(responseDto);
        }

        public Task<UserResponseDto> UpdateUserAsync(Guid userId, UserUpdateDto userUpdateDto)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId && u.UserStatus);
            
            if (user == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            
            if (_users.Any(u => u.Username == userUpdateDto.Username && u.Id != userId))
                throw new Exception("El nombre de usuario ya está en uso por otro cliente.");

            
            user.FullName = userUpdateDto.FullName;
            user.Username = userUpdateDto.Username;

            var responseDto = UserMapper.ToResponseDto(user);
            return Task.FromResult(responseDto);
        }

        public Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId && u.UserStatus);

            if (user == null)
            {
                return Task.FromResult(false);
            }

            user.UserStatus = false;
            return Task.FromResult(true);
        }

        public Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var activeUsers = _users.Select(u => UserMapper.ToResponseDto(u));

            return Task.FromResult(activeUsers);
        }

        // --- MÉTODO DE AUTENTICACIÓN CORREGIDO ---
        // CRÍTICO: Ahora recibe el DTO completo.
        public Task<UserResponseDto> AuthenticateUserAsync(LoginUserDto dto)
        {
            // El campo 'Identifier' en el DTO C# debe coincidir con el JSON 'identifier' de React.
            
            var user = _users.FirstOrDefault(u => 
                (u.Username.Equals(dto.Identifier, StringComparison.OrdinalIgnoreCase) || // Usa dto.Identifier
                u.Email.Equals(dto.Identifier, StringComparison.OrdinalIgnoreCase))       // Usa dto.Identifier
                && u.UserStatus);

            
            if (user == null)
            {
                throw new Exception("Credenciales incorrectas.");
            }

            
            // Verificación de contraseña de texto simple
            if (user.Password != dto.Password) // Usa dto.Password
            {
                throw new Exception("Credenciales incorrectas.");
            }
            
            
            var responseDto = UserMapper.ToResponseDto(user);
            return Task.FromResult(responseDto);
        }
        
    }
}