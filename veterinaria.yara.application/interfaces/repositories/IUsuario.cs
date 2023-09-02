using veterinaria.yara.domain.DTOs;

namespace veterinaria.yara.application.interfaces.repositories
{
    public interface IUsuario
    {
        Task<UsuarioDTO> Login(UsuarioLogeoDTO usuario);
        Task<CrearResponse> CrearUsuario(UsuarioDTO usuario);
    }
}
