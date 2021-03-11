using System;
using System.Collections.Generic;
using AutoMapper;
using ChatApp.Data;
using ChatApp.Dtos;
using ChatApp.Hubs;
using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Services
{
    public class HomeService : IHomeService
    {
        private readonly IChatAppRepo _chatAppRepo;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IMapper _mapper;

        public HomeService(IChatAppRepo chatAppRepo, IHubContext<ChatHub> hubContext, IMapper mapper)
        {
            _chatAppRepo = chatAppRepo;
            _hubContext = hubContext;
            _mapper = mapper;
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
            
            chat.Messages.Add(messageBuilder);
            _chatAppRepo.UpdateDatabase();

            _hubContext.Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", _mapper.Map<ReceiveMessageDto>(messageBuilder));

        }

        public void CreateRoom(string roomName, string userName)
        {
            var userId = _chatAppRepo.GetUserId(userName);
            _chatAppRepo.CreateRoom(roomName, userId);
        }

        public IEnumerable<Chat> GetAllRooms()
        {
            
            return _chatAppRepo.GetAllRooms();
        }

        public IEnumerable<Chat> GetNotJoinedRooms(string userName)
        {
            
            return _chatAppRepo.GetNotJoinedRooms(userName);
        }

        public Chat GetChat(int id)
        {
            return _chatAppRepo.GetChat(id);
        }

        public void UpdateDatabase()
        {
            _chatAppRepo.UpdateDatabase();
        }

        public IEnumerable<Chat> GetJoinedRooms(string userName)
        {
            throw new NotImplementedException();
        }

        public void AddToChat(string userName, int chatId)
        {
            var chat = _chatAppRepo.GetChat(chatId);
            var user = _chatAppRepo.GetUser(_chatAppRepo.GetUserId(userName));
            
            chat.Users.Add(user);
            _chatAppRepo.UpdateDatabase();

        }
    }
}