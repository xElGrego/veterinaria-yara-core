using veterinaria.yara.domain.DTOs;
using veterinaria.yara.domain.DTOs.Paginador;
using veterinaria.yara.domain.DTOs.Raza;

namespace veterinaria.yara.application.interfaces.repositories
{
    public interface IRaza
    {
        Task<List<RazaDTO>> ObtenerRazas();
        Task<PaginationFilterResponse<RazaDTO>> ConsultarRazas(string buscar, int start, int lenght);
        Task<RazaDTO> ConsultarRazaId(Guid idRaza);
        Task<CrearResponse> CrearRaza(NuevaRazaDTO raza);
        Task<CrearResponse> EditarRaza(RazaDTO raza);
        Task<CrearResponse> EliminarRaza(Guid idRaza);
    }
}
