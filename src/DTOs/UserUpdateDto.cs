namespace users_service.src.DTOs
{
    /// <summary>
    /// DTO utilizado para la actualización de datos básicos
    /// de un usuario existente.
    /// </summary>
    public class UserUpdateDto
    {
        /// <summary>
        /// Nuevo nombre completo del usuario.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Nuevo nombre de usuario.
        /// Debe ser único dentro del sistema.
        /// </summary>
        public string Username { get; set; } = string.Empty;
    }
}
