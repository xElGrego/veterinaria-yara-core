using System;
using System.Collections.Generic;

namespace veterinaria.yara.api.DataContext
{
    public partial class Mascota
    {
        public Mascota()
        {
            UsuarioMascota = new HashSet<UsuarioMascota>();
        }

        public Guid IdMascota { get; set; }
        public string? Nombre { get; set; }
        public string? Mote { get; set; }
        public int? Edad { get; set; }
        public decimal? Peso { get; set; }
        public Guid? IdRaza { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool? Estado { get; set; }

        public virtual Raza? IdRazaNavigation { get; set; }
        public virtual ICollection<UsuarioMascota> UsuarioMascota { get; set; }
    }
}
