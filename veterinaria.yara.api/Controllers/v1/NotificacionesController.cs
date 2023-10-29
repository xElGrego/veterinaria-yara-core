using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using veterinaria.yara.application.interfaces.repositories;
using veterinaria.yara.application.models.dtos;
using veterinaria.yara.domain.DTOs;
using Microsoft.AspNetCore.SignalR;
using veterinaria.yara.api.SignalR;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

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

        [HttpGet]
        [Route("/v1/veterinaria-yara/escuchando-usuario")]
        public IActionResult StartListening()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "grego977",
                Password = "yara19975"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "notificacions", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Mensaje" + message);
                    };
                    channel.BasicConsume(queue: "notificacions", autoAck: true, consumer: consumer);
                }
            }
            return Ok("Escuchando la cola de RabbitMQ y enviando notificaciones en tiempo real.");
        }

        private async void SendNotification(string message)
        {
            await _hubContext.Clients.All.SendAsync("Notificar", message);
        }
    }
}
