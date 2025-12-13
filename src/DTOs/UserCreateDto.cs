namespace users_service.src.DTOs
{
    /// <summary>
    /// DTO utilizado para la creación de un nuevo usuario.
    /// Contiene los datos necesarios para registrar un usuario en el sistema.
    /// </summary>
    public class UserCreateDto
    {
        /// <summary>
        /// Nombre completo del usuario.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario.
        /// Debe ser único dentro del sistema.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Nombre de usuario utilizado para autenticación.
        /// Debe ser único dentro del sistema.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña del usuario.
        /// Nota: Se almacena en texto plano únicamente con fines académicos.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de nacimiento del usuario.
        /// </summary>
        public DateOnly DateOfBirth { get; set; }

        /// <summary>
        /// Dirección física o domicilio del usuario.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Número telefónico de contacto del usuario.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
