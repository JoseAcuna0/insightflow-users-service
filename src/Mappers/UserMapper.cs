using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using users_service.src.DTOs;
using users_service.src.models;

namespace users_service.src.Mappers
{
    public class UserMapper
    {
        
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