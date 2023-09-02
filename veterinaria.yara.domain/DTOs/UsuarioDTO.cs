namespace veterinaria.yara.domain.DTOs
{
    public class UsuarioDTO
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string? Correo { get; set; }
        public string Clave { get; set; }
        public string? Token { get; set; }
        public string? Rol { get; set; }
    }
}
