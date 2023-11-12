using System;
using System.Collections.Generic;

namespace veterinaria_yara_core.domain.entities
{
    public partial class Estado
    {
        public Estado()
        {
            Mascota = new HashSet<Mascota>();
            Usuarios = new HashSet<Usuario>();
        }

        public int IdEstado { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Mascota> Mascota { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
