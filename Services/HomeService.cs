using ChatApp.Data;
using ChatApp.Models;

namespace ChatApp.Services
{
    public class HomeService : IHomeService
    {
        private readonly IChatAppRepo _chatAppRepo;

        public HomeService(IChatAppRepo chatAppRepo)
        {
            _chatAppRepo = chatAppRepo;
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
    }
}