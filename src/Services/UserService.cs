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
            var user = _users.FirstOrDefault(u => u.Id == userId && u.UserStatus);
            
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
            var activeUsers = _users.Where(u => u.UserStatus)
                                    .Select(u => UserMapper.ToResponseDto(u));

            return Task.FromResult(activeUsers);
        }
        
    }
}