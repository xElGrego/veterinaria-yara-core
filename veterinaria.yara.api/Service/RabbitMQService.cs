using veterinaria_yara_core.application.interfaces.repositories;

namespace veterinaria_yara_core.api.Service
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
