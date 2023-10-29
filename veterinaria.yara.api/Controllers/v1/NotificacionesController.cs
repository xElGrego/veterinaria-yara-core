using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using veterinaria.yara.application.interfaces.repositories;
using veterinaria.yara.application.models.dtos;
using veterinaria.yara.domain.DTOs;
using Microsoft.AspNetCore.SignalR;
using veterinaria.yara.api.SignalR;


namespace veterinaria.yara.api.Controllers.v1
{
    [Tags("Notificaciones")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class NotificacionesController : BaseApiController
    {
        private readonly INotificaciones _notificaciones;
        private readonly IHubContext<ChatHub> _hubContext;

        public NotificacionesController(INotificaciones notificaciones, IHubContext<ChatHub> hubContext)
        {
            _notificaciones = notificaciones ?? throw new ArgumentNullException(nameof(notificaciones));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
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
        public async Task<ActionResult<MsDtoResponse<CrearResponse>>> NotificacionesOfertas([FromBody][Required] string message)
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
        public async Task<ActionResult<MsDtoResponse<CrearResponse>>> NotificacionUsuario([FromBody][Required] string message, Guid idUsuario)
        {
            var response = await _notificaciones.NotificacionUsuario(message, idUsuario);
            return Ok(response);
        }

    }
}
