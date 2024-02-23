using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using veterinaria_yara_core.application.interfaces.repositories;
using veterinaria_yara_core.application.models.exceptions;
using veterinaria_yara_core.domain.DTOs;
using veterinaria_yara_core.domain.DTOs.Cita;
using veterinaria_yara_core.domain.DTOs.Mascota;
using veterinaria_yara_core.domain.DTOs.Paginador;
using veterinaria_yara_core.domain.DTOs.Raza;
using veterinaria_yara_core.domain.entities;

namespace veterinaria_yara_core.infrastructure.data.repositories
{
    public class CitaRepository : ICita
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly ILogger<CitaRepository> _logger;

        public CitaRepository(IConfiguration configuration, IMapper mapper, DataContext dataContext, ILogger<CitaRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaginationFilterResponse<CitaDTO>> ConsultaCitasUsuario(int start, int length, Guid idUsuario, CancellationToken cancellationToken)
        {
            PaginationFilterResponse<CitaDTO> citas = new();
            try
            {
                var totalRegistros = await _dataContext.Citas
                    .Where(x => x.IdUsuario == idUsuario)
                    .CountAsync();

                var citasQuery = _dataContext.Citas
                 .OrderBy(x => x.Fecha)
                 .Select(x => new CitaDTO
                 {
                     IdCita = x.IdCita,
                     IdEstadoCita = x.IdEstadoCita,
                     IdMascota = x.IdMascota,
                     IdTipoCita = x.IdTipoCita,
                     IdUsuario = x.IdUsuario,
                     Comentarios = x.Comentarios,
                     Fecha = x.Fecha,
                 })
                 .Where(x => x.IdUsuario == idUsuario)
                 .Skip(start)
                 .Take(length);

                citas = await citasQuery.PaginationAsync(start, length, totalRegistros, _mapper);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Consultar citas por usuario", ex.Message);
                throw new VeterinariaYaraException(ex.Message);
            }
            return citas;
        }

        public async Task<List<CitaDTO>> ConsultarCitasDia(DateTime dia)
        {
            var result = new List<CitaDTO>();
            try
            {
                var citasDia = await _dataContext.Citas
                    .Where(x => x.Fecha.Date == dia.Date && x.Fecha.TimeOfDay <= TimeSpan.FromHours(12))
                    .ToListAsync();

                result = _mapper.Map<List<CitaDTO>>(citasDia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Consultar citas por dia", ex.Message);
                throw new VeterinariaYaraException(ex.Message);
            }
            return result;
        }

        public async Task<CrearResponse> CrearCita(NuevaCitaDTO citaParam)
        {
            try
            {
                var cita = _mapper.Map<Cita>(citaParam);
                _dataContext.Citas.Add(cita);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Crear cita [" + JsonConvert.SerializeObject(citaParam) + "]", ex);
                throw new VeterinariaYaraException(ex.Message);
            }

            var response = new CrearResponse
            {
                Response = "La cita fue creada con éxito"
            };

            return response;
        }
    }
}
