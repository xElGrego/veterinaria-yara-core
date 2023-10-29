namespace veterinaria.yara.api.SignalR
{
    public class RabbitMQBackgroundService : BackgroundService
    {
        private readonly RabbitMQConsumer _rabbitMQConsumer;

        public RabbitMQBackgroundService(RabbitMQConsumer rabbitMQConsumer)
        {
            _rabbitMQConsumer = rabbitMQConsumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _rabbitMQConsumer.Consume();
            await Task.CompletedTask;
        }
    }
}
