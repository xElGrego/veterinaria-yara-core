using veterinaria_yara_core.application.interfaces.repositories;
using RabbitMQ.Client;
using System.Text;
using veterinaria_yara_core.application.models.exceptions;
using Microsoft.Extensions.Logging;

namespace veterinaria_yara_core.infrastructure.data.repositories.notificaciones
{
    public class INotificacionesRepository : INotificaciones
    {
        private ILogger<INotificacionesRepository> _logger;

        public INotificacionesRepository(ILogger<INotificacionesRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> NotificacionOfertas(string message)
        {
            try
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
                        channel.ExchangeDeclare(exchange: "notificacions", type: ExchangeType.Fanout, arguments: null);
                        channel.QueueDeclare(queue: "queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
                        channel.QueueBind(queue: "queue", exchange: "notificacions", routingKey: "");
                        var body = Encoding.UTF8.GetBytes(message);
                        var properties = channel.CreateBasicProperties();
                        channel.BasicPublish(exchange: "notificacions", routingKey: "", basicProperties: properties, body: body);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al notificar" + ex.Message);
                throw new VeterinariaYaraException("Error, Notificaciones ofertas", ex.Message);
            }
        }

        public async Task<bool> NotificacionUsuario(string message, Guid idUsuario)
        {
            try
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
                        channel.ExchangeDeclare(exchange: "notificacions_user", type: ExchangeType.Direct);
                        var queueName = "user_queue_" + idUsuario.ToString();
                        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                        channel.QueueBind(queue: queueName, exchange: "notificacions_user", routingKey: idUsuario.ToString());
                        var body = Encoding.UTF8.GetBytes(message);
                        var properties = channel.CreateBasicProperties();
                        channel.BasicPublish(exchange: "notificacions_user", routingKey: idUsuario.ToString(), basicProperties: properties, body: body);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new VeterinariaYaraException($"Error, Notificaciones usuario :{idUsuario}", ex.Message);
            }
        }

    }
}
