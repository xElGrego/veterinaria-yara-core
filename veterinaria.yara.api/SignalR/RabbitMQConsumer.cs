using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace veterinaria.yara.api.SignalR
{
    public class RabbitMQConsumer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<RabbitMQConsumer> _logger;
        private readonly IHubContext<ChatHub> _hubContext;


        public RabbitMQConsumer(ILogger<RabbitMQConsumer> logger, IHubContext<ChatHub> hubContext)
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


        public void Consume()
        {
            try
            {
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    _logger.LogInformation("Message: " + message);
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


