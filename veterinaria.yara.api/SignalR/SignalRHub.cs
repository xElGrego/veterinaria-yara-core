using Microsoft.AspNetCore.SignalR;
using veterinaria.yara.application.interfaces.repositories;
using veterinaria.yara.domain.DTOs;

namespace veterinaria.yara.api.SignalR
{
    public class SignalHub : Hub
    {
        private static Dictionary<string, string> _clientConnections = new Dictionary<string, string>();
        private IChat _chat;

        public SignalHub(IChat chat)
        {
            _chat = chat ?? throw new ArgumentNullException(nameof(chat));
        }

        public async Task EnviarMensaje(string receptor, string emisor, string mensaje)
        {
            //var remitenteId = Context.ConnectionId;
            //var destinatarioConnectionId = _clientConnections.FirstOrDefault(x => x.Value == destinatarioId).Key;

            //var remitenteId = Context.ConnectionId;

            var mensajeDto = new MensajeDTO
            {
                
                RemitenteId = Guid.Parse(emisor),
                DestinatarioId = Guid.Parse(receptor),
                Contenido = mensaje,
                FechaEnvio = DateTime.Now
            };

            await _chat.Insertar(mensajeDto);

            //if (destinatarioConnectionId != null)
            //{
            //    await Clients.Client(destinatarioConnectionId).SendAsync("RecibirMensaje", remitenteId, mensaje);
            //}
        }

        public override Task OnConnectedAsync()
        {
            string clientId = Context.ConnectionId;
            _clientConnections[clientId] = "ID_DEL_USUARIO";
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}


