using veterinaria_yara_core.domain.DTOs;
using veterinaria_yara_core.domain.DTOs.Paginador;
using veterinaria_yara_core.domain.DTOs.Raza;

namespace veterinaria_yara_core.application.interfaces.repositories
{
    public interface IRaza
    {
        Task<List<RazaDTO>> ObtenerRazas();
        Task<PaginationFilterResponse<RazaDTO>> ConsultarRazas(string buscar, int start, int length);
        Task<RazaDTO> ConsultarRazaId(Guid idRaza);
        Task<CrearResponse> CrearRaza(NuevaRazaDTO raza);
        Task<CrearResponse> EditarRaza(RazaDTO raza);
        Task<CrearResponse> EliminarRaza(Guid idRaza);
    }
}
