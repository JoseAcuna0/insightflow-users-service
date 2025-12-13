using System.Text.Json.Serialization;

namespace users_service.src.DTOs
{
    /// <summary>
    /// DTO utilizado para la autenticación de usuarios.
    /// Permite iniciar sesión utilizando nombre de usuario
    /// o correo electrónico junto con la contraseña.
    /// </summary>
    public class LoginUserDto
    {
        /// <summary>
        /// Identificador del usuario.
        /// Puede corresponder al nombre de usuario o al correo electrónico.
        /// </summary>
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }
}
