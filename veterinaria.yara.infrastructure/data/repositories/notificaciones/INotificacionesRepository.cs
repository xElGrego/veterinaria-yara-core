using veterinaria.yara.application.interfaces.repositories;
using RabbitMQ.Client;
using System.Text;
using veterinaria.yara.application.models.exceptions;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Logging;
using veterinaria.yara.domain.entities;

namespace veterinaria.yara.infrastructure.data.repositories.notificaciones
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
                        channel.ExchangeDeclare("notificacions", ExchangeType.Fanout, arguments: null);
                        var body = Encoding.UTF8.GetBytes(message);
                        var properties = channel.CreateBasicProperties();
                        channel.BasicPublish("notificacions", "", properties, body);
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
                        var queueName = channel.QueueDeclare().QueueName;
                        // Vincula la cola al intercambio con la clave de enrutamiento igual al ID del usuario
                        channel.QueueBind(queue: queueName, exchange: "notificacions_user", routingKey: idUsuario.ToString());
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "notificacions_user", routingKey: idUsuario.ToString(), basicProperties: null, body: body);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new VeterinariaYaraException($"Error, Notificaciones usuario :{idUsuario}", ex.Message);
            }
        }


        public async Task Escuchando()
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
                        channel.QueueDeclare(queue: "notificacions_user", durable: false, exclusive: false, autoDelete: false, arguments: null);

                        var consumer = new EventingBasicConsumer(channel);

                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);
                            _logger.LogInformation("Mensaje" + message);
                        };
                        channel.BasicConsume(queue: "notificacions_user", autoAck: true, consumer: consumer);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new VeterinariaYaraException($"Error, Escuchado", ex.Message);
            }
        }
    }
}
