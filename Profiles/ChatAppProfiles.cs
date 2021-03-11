using AutoMapper;
using ChatApp.Dtos;
using ChatApp.Models;

namespace ChatApp.Profiles
{
    public class ChatAppProfiles : Profile
    {
        public ChatAppProfiles()
        {
            // Source => Target
            CreateMap<Message, ReceiveMessageDto>();
        }
    }
}