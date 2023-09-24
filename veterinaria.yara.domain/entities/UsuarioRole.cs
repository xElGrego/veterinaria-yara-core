using System;
using System.Collections.Generic;

namespace veterinaria.yara.domain.entities
{
    public partial class UsuarioRole
    {
        public Guid IdUsuarioRol { get; set; }
        public Guid? IdUsuario { get; set; }
        public Guid? IdRol { get; set; }

        public virtual Role? IdRolNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
