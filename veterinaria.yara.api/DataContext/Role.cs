using System;
using System.Collections.Generic;

namespace veterinaria.yara.api.DataContext
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
