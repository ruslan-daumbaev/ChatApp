using System;
using System.Threading;
using ChatApp.Services.Dto;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatApp.Host.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {
        private static int ConnectedCount;

        public async Task Notify(GetMessageDto message)
        {
            await Clients.All.Notify(message);
        }

        public override async Task OnConnectedAsync()
        {
            Interlocked.Increment(ref ConnectedCount);
            await Clients.All.ConnectedChanged(ConnectedCount);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Interlocked.Decrement(ref ConnectedCount);
            await Clients.All.ConnectedChanged(ConnectedCount);
        }
    }
}