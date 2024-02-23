using System;
using System.Collections.Generic;

namespace veterinaria_yara_core.domain.entities
{
    public partial class Cita
    {
        public Guid IdCita { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentarios { get; set; } = null!;
        public Guid? IdEstadoCita { get; set; }
        public Guid? IdUsuario { get; set; }
        public Guid? IdMascota { get; set; }
        public Guid? IdTipoCita { get; set; }

        public virtual EstadoCitum? IdEstadoCitaNavigation { get; set; }
        public virtual Mascota? IdMascotaNavigation { get; set; }
        public virtual TipoCitum? IdTipoCitaNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
