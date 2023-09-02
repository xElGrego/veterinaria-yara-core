using System;
using System.Collections.Generic;

namespace veterinaria.yara.api.DataContext
{
    public partial class Raza
    {
        public Raza()
        {
            Mascota = new HashSet<Mascota>();
        }

        public Guid IdRaza { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual ICollection<Mascota> Mascota { get; set; }
    }
}
