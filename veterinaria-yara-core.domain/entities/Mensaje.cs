using System;
using System.Collections.Generic;

namespace veterinaria_yara_core.domain.entities
{
    public partial class Mensaje
    {
        public int Id { get; set; }
        public Guid? RemitenteId { get; set; }
        public Guid? DestinatarioId { get; set; }
        public string? Contenido { get; set; }
        public DateTime? FechaEnvio { get; set; }

        public virtual Usuario? Destinatario { get; set; }
        public virtual Usuario? Remitente { get; set; }
    }
}
