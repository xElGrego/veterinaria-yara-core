using veterinaria.yara.application.interfaces.repositories;

namespace veterinaria.yara.api.Service
{
    public class RabbitMQService : BackgroundService
    {
        private readonly IRabbitMQ _rabbit;

        public RabbitMQService(IRabbitMQ rabbit)
        {
            _rabbit = rabbit ?? throw new ArgumentNullException(nameof(rabbit));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _rabbit.Consumer();
            await Task.CompletedTask;
        }
    }
}
