using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace users_service.src.DTOs
{
    public class UserUpdateDto
    {
        public string FullName {get; set;} = string.Empty;

        public string Username {get; set;} = string.Empty;
    }
}