using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using veterinaria_yara_core.application.interfaces.repositories;
using veterinaria_yara_core.application.models.exceptions;
using veterinaria_yara_core.domain.DTOs.Estados;

namespace veterinaria_yara_core.infrastructure.data.repositories.estados
{
    public class EstadosRepository : IEstados
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private ILogger<EstadosRepository> _logger;

        public EstadosRepository(ILogger<EstadosRepository> logger, DataContext dataContext, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<EstadosDTO>> ObtenerEstados()
        {
            List<EstadosDTO> result = new();

            try
            {
                var searchData = await _dataContext.Estados.ToListAsync();
                if (searchData == null)
                {
                    throw new VeterinariaYaraException("No existen estados en la tabla");
                }
                result = _mapper.Map<List<EstadosDTO>>(searchData);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error obtener estados", ex);
                throw new VeterinariaYaraException(ex.Message);
            }

            return result;
        }
    }
}
