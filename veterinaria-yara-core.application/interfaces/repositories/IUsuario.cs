using veterinaria_yara_core.domain.DTOs;
using veterinaria_yara_core.domain.DTOs.Paginador;
using veterinaria_yara_core.domain.DTOs.Usuario;

namespace veterinaria_yara_core.application.interfaces.repositories
{
    public interface IUsuario
    {
        Task<NuevoUsuarioDTO> Login(UsuarioLogeoDTO usuario);
        Task<CrearResponse> CrearUsuario(AgregarUsuarioDTO usuario);
        Task<PaginationFilterResponse<UsuarioDTO>> ConsultarUsuarios(int start, int length, CancellationToken cancellationToken);
    }
}
