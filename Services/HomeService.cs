using System;
using ChatApp.Data;
using ChatApp.Hubs;
using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Services
{
    public class HomeService : IHomeService
    {
        private readonly IChatAppRepo _chatAppRepo;
        private readonly IHubContext<ChatHub> _hubContext;

        public HomeService(IChatAppRepo chatAppRepo, IHubContext<ChatHub> hubContext)
        {
            _chatAppRepo = chatAppRepo;
            _hubContext = hubContext;
        }

        public void BuildMessage(int chatId, string message, string userName)
        {
            // Build the text sent from user into Message model
            var messageBuilder = new Message
            {
                Name = userName,
                Text = message,
                SentAt = DateTime.Now
            };

            var chat = _chatAppRepo.GetChat(chatId);

            // Add message to database 
            chat.Messages.Add(messageBuilder);
            _chatAppRepo.UpdateDatabase();

            // Broadcast message clients connected
            // MAYBE CHANGE TO ROOM CONNECTED CLIENTS - test this first ####################################################
            _hubContext.Clients.All.SendAsync("ReceiveMessage", messageBuilder);

            
        }

        public void CreateRoom(string roomName, string userName)
        {
            var userId = _chatAppRepo.GetUserId(userName);
            _chatAppRepo.CreateRoom(roomName, userId);
        }

        public Chat GetChat(int id)
        {
            return _chatAppRepo.GetChat(id);
        }

        public void UpdateDatabase()
        {
            _chatAppRepo.UpdateDatabase();
        }
    }
}