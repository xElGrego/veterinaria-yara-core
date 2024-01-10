namespace veterinaria_yara_core.domain.DTOs.Usuario
{
    public class AgregarUsuarioDTO
    {
        public Guid IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string? Correo { get; set; }
        public string Clave { get; set; }
        public List<string?> Rol { get; set; }
    }
}
