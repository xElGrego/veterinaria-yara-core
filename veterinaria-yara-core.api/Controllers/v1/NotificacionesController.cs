using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using veterinaria_yara_core.application.interfaces.repositories;
using veterinaria_yara_core.application.models.dtos;
using veterinaria_yara_core.domain.DTOs;

namespace veterinaria_yara_core.api.Controllers.v1
{
    [Tags("Notificaciones")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class NotificacionesController : BaseApiController
    {
        private readonly INotificaciones _notificaciones;

        public NotificacionesController(INotificaciones notificaciones)
        {
            _notificaciones = notificaciones ?? throw new ArgumentNullException(nameof(notificaciones));
        }


        /// <summary>
        /// Método para enviar ofertas a todos los usuarios (rabbitmq: fannout)
        /// </summary>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/notificaciones-ofertas")]
        public async Task<ActionResult<CrearResponse>> NotificacionesOfertas([FromBody][Required] string message)
        {
            var response = await _notificaciones.NotificacionOfertas(message);
            return Ok(response);
        }


        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/notificacion-usuario")]
        public async Task<ActionResult<CrearResponse>> NotificacionUsuario([FromBody][Required] string message, Guid idUsuario)
        {
            var response = await _notificaciones.NotificacionUsuario(message, idUsuario);
            return Ok(response);
        }
    }
}
