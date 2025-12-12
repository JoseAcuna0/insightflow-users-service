using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace users_service.src.DTOs
{
    public class LoginUserDto
    {
        public string Identifier { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}