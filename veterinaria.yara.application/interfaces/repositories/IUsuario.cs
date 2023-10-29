using veterinaria.yara.domain.DTOs;
using veterinaria.yara.domain.DTOs.Paginador;
using veterinaria.yara.domain.DTOs.Usuario;

namespace veterinaria.yara.application.interfaces.repositories
{
    public interface IUsuario
    {
        Task<NuevoUsuarioDTO> Login(UsuarioLogeoDTO usuario);
        Task<CrearResponse> CrearUsuario(AgregarUsuarioDTO usuario);
        Task<PaginationFilterResponse<UsuarioDTO>> ConsultarUsuarios(int start, int length, CancellationToken cancellationToken);
    }
}
