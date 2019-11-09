using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace autochat_blazorapp.Hubs
{
    public class ChatHub : Hub
    {
        /// <summary>
        /// connectionId-to-username lookup
        /// </summary>
        /// <remarks>
        /// Needs to be static as the chat is created dynamically a lot
        /// </remarks>
        private static readonly Dictionary<string, string> userLookup = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> userIdLookup = new Dictionary<string, string>();

        /// <summary>
        /// Send a message to all clients
        /// </summary>
        /// <param name="username"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string username, string message, string toId)
        {
            //1대1로 변경
            if (userIdLookup.TryGetValue(toId, out string connectionId))
            {
                await Clients.Client(connectionId)?.SendAsync("ReceiveMessage", username, message);
            }


            //await Clients.All.SendAsync("ReceiveMessage", username, message);
        }

        /// <summary>
        /// Register username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task Register(string userId)
        {
            var currentId = Context.ConnectionId;
            if (!userLookup.ContainsKey(currentId))
            {
                // maintain a lookup of connectionId-to-username
                userLookup.Add(currentId, userId);

                userIdLookup[userId] = currentId;

                // re-use existing message for now
                //await Clients.AllExcept(currentId).SendAsync("ReceiveMessage", userId, $"{userId} joined the chat");
            }
        }

        /// <summary>
        /// Log connection
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            
            Console.WriteLine("---------------- Connected");
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// Log disconnection
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message}");
            // try to get connection
            string id = Context.ConnectionId;
            if (userLookup.TryGetValue(id, out string userId))
            {
                userLookup.Remove(id);

                if (userIdLookup.ContainsKey(userId))
                {
                    userIdLookup.Remove(userId);
                }

                //await Clients.AllExcept(Context.ConnectionId).SendAsync("ReceiveMessage", username, $"{username} has left the chat");
            }



            await base.OnDisconnectedAsync(e);
        }

    }
}
