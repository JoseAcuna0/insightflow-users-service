using System;

namespace users_service.src.models
{
    /// <summary>
    /// Representa la entidad de dominio Usuario dentro del sistema.
    /// Contiene la información básica de identificación, contacto
    /// y estado del usuario.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identificador único del usuario.
        /// Se genera automáticamente al momento de su creación.
        /// </summary>
        public Guid Id { get; set; }

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
        /// Contraseña del usuario.
        /// Nota: En este proyecto se almacena en texto plano
        /// únicamente con fines académicos.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Nombre de usuario utilizado para autenticación.
        /// Debe ser único dentro del sistema.
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

        /// <summary>
        /// Número telefónico de contacto del usuario.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
