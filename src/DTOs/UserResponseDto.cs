namespace users_service.src.DTOs
{
    /// <summary>
    /// DTO utilizado para devolver la información de un usuario
    /// como respuesta desde la API.
    /// Contiene únicamente los datos públicos del usuario.
    /// </summary>
    public class UserResponseDto
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre completo del usuario.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Nombre de usuario del usuario.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Estado del usuario dentro del sistema.
        /// True indica usuario activo, false indica eliminación lógica.
        /// </summary>
        public bool UserStatus { get; set; }

        /// <summary>
        /// Fecha de nacimiento del usuario.
        /// </summary>
        public DateOnly DateOfBirth { get; set; }

        /// <summary>
        /// Dirección física o domicilio del usuario.
        /// </summary>
        public string Address { get; set; } = string.Empty;
    }
}
