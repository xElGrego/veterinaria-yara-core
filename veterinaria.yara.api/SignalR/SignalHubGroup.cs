using Microsoft.AspNetCore.SignalR;

namespace veterinaria.yara.api.SignalR
{
    public class SignalHubGroup : Hub
    {
        public async Task EnviarMensaje(string user, string message)
        {
            await Clients.All.SendAsync("Respuesta de signal R", user, message);
        }

        public async Task JoinGroup(string groupName, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("NewUser", $"{userName} entró al canal");
        }

        public async Task LeaveGroup(string groupName, string userName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("LeftUser", $"{userName} salió del canal");
        }

        public async Task SendMessage(NewMessageGroup message)
        {
            await Clients.Group(message.GroupName).SendAsync("NewMessage", message);
        }

    }
}

public record NewMessageGroup(string UserName, string Message, string GroupName);
