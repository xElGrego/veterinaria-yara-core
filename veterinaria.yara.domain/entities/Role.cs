using System;
using System.Collections.Generic;

namespace veterinaria.yara.domain.entities
{
    public partial class Role
    {
        public Role()
        {
            UsuarioRoles = new HashSet<UsuarioRole>();
        }

        public Guid IdRol { get; set; }
        public string NombreRol { get; set; } = null!;

        public virtual ICollection<UsuarioRole> UsuarioRoles { get; set; }
    }
}
