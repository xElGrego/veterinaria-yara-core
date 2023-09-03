using veterinaria.yara.domain.DTOs;
using veterinaria.yara.domain.DTOs.Mascota;
using veterinaria.yara.domain.DTOs.Paginador;

namespace veterinaria.yara.application.interfaces.repositories
{
    public interface IMascota
    {
        Task<PaginationFilterResponse<MascotaDTO>> ConsultarMascotas(int skip, int take, Guid? idUsuario, CancellationToken cancellationToken);
        Task<MascotaDTO> ConsultarMascotaId(Guid idMascota);
        Task<CrearResponse> CrearMascota(NuevaMascotaDto mascota);
        Task<CrearResponse> EditarMascota(MascotaDTO mascota);
        Task<CrearResponse> EliminarMascota(Guid idMascota);
    }
}
