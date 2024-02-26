using veterinaria_yara_core.domain.DTOs;
using veterinaria_yara_core.domain.DTOs.Cita;
using veterinaria_yara_core.domain.DTOs.Paginador;

namespace veterinaria_yara_core.application.interfaces.repositories
{
    public interface ICita
    {
        Task<PaginationFilterResponse<CitaDTO>> ConsultaCitasUsuario(int start, int length, Guid idUsuario, CancellationToken cancellationToken);
        Task<List<CitaDTO>> ConsultarCitasDia(DateTime dia);
        Task<CrearResponse> CrearCita(NuevaCitaDTO cita);
        Task<CrearResponse> ActualizarCita(Guid idCita, string estadoCita);
    }
}
