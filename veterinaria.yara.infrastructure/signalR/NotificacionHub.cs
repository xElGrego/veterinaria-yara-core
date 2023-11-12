using Microsoft.AspNetCore.SignalR;

namespace veterinaria_yara_core.infrastructure.signalR
{
    public class NotificacionHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
