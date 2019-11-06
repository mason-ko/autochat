using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace autochat_blazorapp.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("------------------- on Connected");

            //SendMessage("system", $"{Context.ConnectionId} joined");

            

            return base.OnConnectedAsync();
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessageAsync(string user, string message)
        {
            await this.Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
