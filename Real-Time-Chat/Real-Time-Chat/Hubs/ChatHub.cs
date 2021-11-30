using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Real_Time_Chat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly string _botUser;

        public ChatHub()
        {
            this._botUser = "My Chatbot";
        }

        public async Task joinRoom (UserConnection userCon)
        {
            Console.WriteLine("joining room");
            await Groups.AddToGroupAsync(Context.ConnectionId, userCon.room);
            await Clients.Group(userCon.room).SendAsync("ReceiveMessage", _botUser, $"{userCon.user} has joined {userCon.room}");
        }
    }
}
