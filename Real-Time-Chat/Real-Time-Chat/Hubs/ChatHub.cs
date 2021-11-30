using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Real_Time_Chat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly string _botUser;
        private readonly IDictionary<string, UserConnection> _connections;

        public ChatHub(IDictionary<string, UserConnection> connections)
        {
            this._botUser = "My Chatbot";
            this._connections = connections;
        }

        public async Task joinRoom (UserConnection userCon)
        {
            
            _connections[Context.ConnectionId] = userCon;

            await Groups.AddToGroupAsync(Context.ConnectionId, userCon.room);
            await Clients.Group(userCon.room).SendAsync("ReceiveMessage", _botUser, $"{userCon.user} has joined {userCon.room}");
        }

        public async Task sendMessage(string msg)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userCon))
            {
                await Clients.Group(userCon.room)
                    .SendAsync("ReceiveMessage", userCon.user, msg);
            }
        }
    }
}
