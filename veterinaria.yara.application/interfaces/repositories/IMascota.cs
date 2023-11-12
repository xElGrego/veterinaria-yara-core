using veterinaria_yara_core.domain.DTOs;
using veterinaria_yara_core.domain.DTOs.Estados.Mascota;
using veterinaria_yara_core.domain.DTOs.Paginador;

namespace veterinaria_yara_core.application.interfaces.repositories
{
    public interface IMascota
    {
        Task<PaginationFilterResponse<MascotaDTO>> ConsultarMascotas(int start, int length, string nombre, int estado, DateTime fechaInicio, DateTime fechaFin, Guid? idUsuario, CancellationToken cancellationToken);

        Task<PaginationFilterResponse<MascotaDTO>> ConsultarMascotasUsuario(int start, int length, Guid idUsuario, CancellationToken cancellationToken);
        Task<MascotaDTO> ConsultarMascotaId(Guid idMascota);
        Task<CrearResponse> CrearMascota(NuevaMascotaDto mascota);
        Task<CrearResponse> EditarMascota(MascotaDTO mascota);
        Task<CrearResponse> ActivarMascota(Guid idMascota);
        Task<CrearResponse> EliminarMascota(Guid idMascota);
        Task<MascotaDTO> UltimaMascota(Guid idUsuario);
        Task<CrearResponse> ReordenarMascota(List<ReordenarMascotaDTO> mascotas);
    }
}
