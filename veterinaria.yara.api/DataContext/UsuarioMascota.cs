using System;
using System.Collections.Generic;

namespace veterinaria.yara.api.DataContext
{
    public partial class UsuarioMascota
    {
        public Guid IdUsuarioMascota { get; set; }
        public Guid? IdUsuario { get; set; }
        public Guid? IdMascota { get; set; }

        public virtual Mascota? IdMascotaNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
