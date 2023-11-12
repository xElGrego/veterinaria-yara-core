using System;
using System.Collections.Generic;

namespace veterinaria_yara_core.domain.entities
{
    public partial class Mascota
    {
        public Mascota()
        {
            Historials = new HashSet<Historial>();
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
        public int? Estado { get; set; }
        /// <summary>
        /// Sirve para tener el orden de posicion en el front
        /// </summary>
        public int? Orden { get; set; }

        public virtual Estado? EstadoNavigation { get; set; }
        public virtual Raza? IdRazaNavigation { get; set; }
        public virtual ICollection<Historial> Historials { get; set; }
        public virtual ICollection<UsuarioMascota> UsuarioMascota { get; set; }
    }
}
