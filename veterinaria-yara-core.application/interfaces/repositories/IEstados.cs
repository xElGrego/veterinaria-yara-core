using veterinaria_yara_core.domain.DTOs.Estados;

namespace veterinaria_yara_core.application.interfaces.repositories
{
    public interface IEstados
    {
        Task<List<EstadosDTO>> ObtenerEstados();
    }
}
