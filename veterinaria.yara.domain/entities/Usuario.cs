namespace veterinaria.yara.domain.entities
{
    public partial class Usuario
    {
        public Usuario()
        {
            MensajeDestinatarios = new HashSet<Mensaje>();
            MensajeRemitentes = new HashSet<Mensaje>();
            UsuarioMascota = new HashSet<UsuarioMascota>();
            UsuarioRoles = new HashSet<UsuarioRole>();
        }

        public Guid IdUsuario { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? Correo { get; set; }
        public bool? Estado { get; set; }
        public virtual ICollection<Mensaje> MensajeDestinatarios { get; set; }
        public virtual ICollection<Mensaje> MensajeRemitentes { get; set; }
        public virtual ICollection<UsuarioMascota> UsuarioMascota { get; set; }
        public virtual ICollection<UsuarioRole> UsuarioRoles { get; set; }
    }
}
