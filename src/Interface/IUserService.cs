using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using users_service.src.DTOs;

namespace users_service.src.Interface
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateUserAsync(UserCreateDto userCreateDto);

        Task<UserResponseDto> UpdateUserAsync(Guid userId, UserUpdateDto userUpdateDto);

        Task<UserResponseDto> GetUserByIdAsync(Guid userId);

        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();

        Task<bool> DeleteUserAsync(Guid userId);

        Task<UserResponseDto> AuthenticateUserAsync(string usernameOrEmail, string password);
    }
}