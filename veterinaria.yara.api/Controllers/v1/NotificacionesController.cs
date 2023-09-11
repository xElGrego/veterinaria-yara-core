using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using veterinaria.yara.application.interfaces.repositories;
using veterinaria.yara.application.models.dtos;
using veterinaria.yara.domain.DTOs.Raza;
using veterinaria.yara.domain.DTOs;

namespace veterinaria.yara.api.Controllers.v1
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
