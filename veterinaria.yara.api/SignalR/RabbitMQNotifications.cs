using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace veterinaria.yara.api.SignalR
{
    public class RabbitMQNotificationService : BackgroundService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ILogger<RabbitMQNotificationService> _logger;

        public RabbitMQNotificationService(IHubContext<ChatHub> hubContext, ILogger<RabbitMQNotificationService> logger)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "grego977",
                Password = "yara19975"
            };

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var connection = factory.CreateConnection())
                    using (var channel = connection.CreateModel())
                    {
                        //channel.ExchangeDeclare(exchange: "notificacions", type: ExchangeType.Fanout);
                        channel.ExchangeDeclare("notificacions", ExchangeType.Fanout);
                        channel.QueueDeclare("queue",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);


                        channel.QueueBind("queue", "notificacions", string.Empty);
                        //channel.BasicQos(0, 10, false);

                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);
                            _logger.LogInformation("Mensaje: " + message);
                            _hubContext.Clients.All.SendAsync("Notificar", message);
                        };

                        channel.BasicConsume(queue: "queue", autoAck: true, consumer: consumer);
                    }

                    await Task.Delay(10000, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error en el servicio en segundo plano.");
                }
            }
        }
    }
}
