using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message)
        {
           // Console.WriteLine(message);
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}