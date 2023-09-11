using veterinaria.yara.application.interfaces.repositories;
using RabbitMQ.Client;
using System.Text;
using veterinaria.yara.application.models.exceptions;

namespace veterinaria.yara.infrastructure.data.repositories.notificaciones
{
    public class INotificacionesRepository : INotificaciones
    {
        public async Task<bool> NotificacionOfertas(string message)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = "localhost",
                    UserName = "grego",
                    Password = "yara19975"
                };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: "notificacions", type: ExchangeType.Fanout);
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "notificacions", routingKey: "", basicProperties: null, body: body);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
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
                    UserName = "grego",
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
    }
}
