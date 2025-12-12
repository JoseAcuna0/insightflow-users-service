using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace users_service.src.DTOs
{
    public class LoginUserDto
    {
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }
}