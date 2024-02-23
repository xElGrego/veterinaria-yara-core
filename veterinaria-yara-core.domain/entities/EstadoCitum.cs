using System;
using System.Collections.Generic;

namespace veterinaria_yara_core.domain.entities
{
    public partial class EstadoCitum
    {
        public EstadoCitum()
        {
            Cita = new HashSet<Cita>();
        }

        public Guid IdEstadoCita { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual ICollection<Cita> Cita { get; set; }
    }
}
