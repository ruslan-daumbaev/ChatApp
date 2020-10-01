using ChatApp.Services.Dto;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatApp.Host.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {
        public async Task Notify(GetMessageDto message)
        {
            await Clients.All.Notify(message);
        }
    }
}