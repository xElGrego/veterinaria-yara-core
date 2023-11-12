using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using veterinaria_yara_core.application.models.exceptions;
using veterinaria_yara_core.domain.DTOs;
using veterinaria_yara_core.domain.entities;
using veterinaria_yara_core.application.interfaces.repositories;

namespace veterinaria_yara_core.infrastructure.data.repositories.chat
{
    public class Chat : IChat
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private ILogger<Chat> _logger;

        public Chat(ILogger<Chat> logger, DataContext dataContext, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Insertar(MensajeDTO mensaje)
        {
            try
            {
                var mapper = _mapper.Map<Mensaje>(mensaje);
                _dataContext.Mensajes.Add(mapper);
                _logger.LogInformation("Insertar mensaje [" + JsonConvert.SerializeObject(mapper) + "]");
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error insertar mensaje [" + JsonConvert.SerializeObject(mensaje) + "]", ex);
                throw new VeterinariaYaraException(ex.Message);
            }
        }
    }
}
