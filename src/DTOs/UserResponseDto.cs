using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace users_service.src.DTOs
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public bool UserStatus { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Address { get; set; } = string.Empty;
    }
}