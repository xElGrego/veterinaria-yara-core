using System;
using System.Collections.Generic;

namespace veterinaria.yara.domain.entities
{
    public partial class Historial
    {
        public Guid IdHistorial { get; set; }
        public DateTime FechaEvento { get; set; }
        public string? Descripcion { get; set; }
        public Guid IdTipoEvento { get; set; }
        public Guid? IdMascota { get; set; }

        public virtual Mascota? IdMascotaNavigation { get; set; }
        public virtual TipoEvento IdTipoEventoNavigation { get; set; } = null!;
    }
}
