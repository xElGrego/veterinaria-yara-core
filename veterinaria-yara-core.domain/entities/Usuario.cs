using System;
using System.Collections.Generic;

namespace veterinaria_yara_core.domain.entities
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
        public int? Estado { get; set; }
        public virtual EstadoUsuario? IdEstadoUsuarioNavigation { get; set; }
        public virtual ICollection<Cita> Cita { get; set; }
        public virtual ICollection<Mensaje> MensajeDestinatarios { get; set; }
        public virtual ICollection<Mensaje> MensajeRemitentes { get; set; }
        public virtual ICollection<UsuarioMascota> UsuarioMascota { get; set; }
        public virtual ICollection<UsuarioRole> UsuarioRoles { get; set; }
    }
}
