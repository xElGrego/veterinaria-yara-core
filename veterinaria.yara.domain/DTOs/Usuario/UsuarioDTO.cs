namespace veterinaria.yara.domain.DTOs.Usuario
{
    public class UsuarioDTO
    {
        public Guid IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public List<string>? Rol { get; set; }
    }
}
