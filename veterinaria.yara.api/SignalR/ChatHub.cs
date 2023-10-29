using Microsoft.AspNetCore.SignalR;

namespace veterinaria.yara.api.SignalR
{
    public class ChatHub : Hub
    {
        public async Task EnviarMensaje(string usuario, string mensaje)
        {
            // Puedes realizar acciones específicas relacionadas con la recepción de mensajes
            // o cualquier otra lógica que necesites.
            // Luego, envía un mensaje a todos los clientes conectados al hub.
            await Clients.All.SendAsync("RecibirMensaje", usuario, mensaje);
        }

        public override async Task OnConnectedAsync()
        {
            // Este método se ejecuta cuando un cliente se conecta al hub.
            // Puedes realizar acciones específicas relacionadas con la conexión de clientes.
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Este método se ejecuta cuando un cliente se desconecta del hub.
            // Puedes realizar acciones específicas relacionadas con la desconexión de clientes.
            await base.OnDisconnectedAsync(exception);
        }
    }
}
