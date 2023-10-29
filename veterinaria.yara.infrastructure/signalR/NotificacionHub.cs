using Microsoft.AspNetCore.SignalR;

namespace veterinaria.yara.infrastructure.signalR
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
