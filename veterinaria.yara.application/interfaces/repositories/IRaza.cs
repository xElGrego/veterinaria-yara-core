using veterinaria.yara.domain.DTOs;
using veterinaria.yara.domain.DTOs.Paginador;

namespace veterinaria.yara.application.interfaces.repositories
{
    public interface IRaza
    {
        Task<PaginationFilterResponse<RazaDto>> ConsultarRazas(string buscar, int start, int lenght);
        Task<RazaDto> ConsultarRazaId(Guid idRaza);
        Task<CrearResponse> CrearRaza(RazaDto raza);
        Task<CrearResponse> EditarRaza(RazaDto raza);
        Task<CrearResponse> EliminarRaza(Guid idRaza);
    }
}
