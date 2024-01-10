using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using veterinaria_yara_core.application.interfaces.repositories;
using System.Text;
using veterinaria_yara_core.infrastructure.signalR;

namespace veterinaria_yara_core.infrastructure.data.repositories.rabbitmq
{
    public class RabbitMQRepository : IRabbitMQ
    {

        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<RabbitMQRepository> _logger;
        private readonly IHubContext<NotificacionHub> _hubContext;

        public RabbitMQRepository(ILogger<RabbitMQRepository> logger, IHubContext<NotificacionHub> hubContext)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "grego977",
                Password = "yara19975"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("notificacions", ExchangeType.Fanout);
            _channel.QueueDeclare(queue: "queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.QueueBind(queue: "queue", exchange: "notificacions", routingKey: string.Empty);
        }

        public void Consumer()
        {
            try
            {
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    _hubContext.Clients.All.SendAsync("Notificar", message);
                };
                _channel.BasicConsume(queue: "queue", autoAck: true, consumer: consumer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el servicio en segundo plano.");
            }
        }
    }
}
