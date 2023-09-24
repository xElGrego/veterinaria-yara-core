using veterinaria.yara.domain.DTOs.Estados;

namespace veterinaria.yara.application.interfaces.repositories
{
    public interface IEstados
    {
        Task<List<EstadosDTO>> ObtenerEstados();
    }
}
