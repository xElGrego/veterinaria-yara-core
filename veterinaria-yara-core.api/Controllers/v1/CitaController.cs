using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using veterinaria_yara_core.application.interfaces.repositories;
using veterinaria_yara_core.application.models.dtos;
using veterinaria_yara_core.domain.DTOs;
using veterinaria_yara_core.domain.DTOs.Cita;
using veterinaria_yara_core.domain.DTOs.Paginador;

namespace veterinaria_yara_core.api.Controllers.v1
{
    [Tags("Cita")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly ICita _citaRepository;
        public CitaController(ICita citaRepository)
        {
            _citaRepository = citaRepository ?? throw new ArgumentNullException(nameof(citaRepository));
        }


        /// <summary>
        /// Genera la lista de citas paginadas por usuario
        /// </summary>
        /// <param name="start"> Número de páginan dodne se requiere empezar la consulta </param>
        /// <param name="length"> Cantidad de items que se requiere obtener </param>
        /// <param name="idUsuario"> Id usuario </param>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaginationFilterResponse<CitaDTO>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/consulta-citas-usuarios")]
        public async Task<ActionResult<PaginationFilterResponse<CitaDTO>>> ConsultarCitasUsuario(int start, Int16 length, Guid idUsuario,
        CancellationToken cancellationToken)
        {
            var response = await _citaRepository.ConsultaCitasUsuario(start, length, idUsuario, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Genera la lista de citas paginadas por usuario
        /// </summary>
        /// <param name="dia">Dia exacto para traer las citas que estén agendadas.</param>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaginationFilterResponse<CitaDTO>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/consulta-citas-dia")]
        public async Task<ActionResult<PaginationFilterResponse<CitaDTO>>> ConsultarCitasDia(DateTime dia)
        {
            var response = await _citaRepository.ConsultarCitasDia(dia);
            return Ok(response);
        }


        /// <summary>
        /// Método para editar el estado de la cita
        /// </summary>
        [HttpPut]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CrearResponse), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/actualizar-estado-cita")]
        public async Task<ActionResult<CrearResponse>> ActualizarCita([FromBody][Required] ActualizaCitaDTO cita)
        {
            var response = await _citaRepository.ActualizarCita(cita.IdCita, cita.EstadoCita);
            return Ok(response);
        }


        /// <summary>
        /// Método para crear citas 
        /// </summary>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CrearResponse), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/crear-cita")]
        public async Task<ActionResult<CrearResponse>> CrearCita([FromBody][Required] NuevaCitaDTO cita)
        {
            var response = await _citaRepository.CrearCita(cita);
            return Ok(response);
        }
    }
}
