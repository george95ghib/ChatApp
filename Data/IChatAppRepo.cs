using System.Collections.Generic;
using ChatApp.Models;

namespace ChatApp.Data
{
    public interface IChatAppRepo 
    {
        Chat GetChat(int id);
        void CreateRoom(string roomName, string userId);
        string GetUserId(string userName);
        User GetUser(string userId);
        IEnumerable<Chat> GetAllRooms();
        IEnumerable<Chat> GetNotJoinedRooms(string userName);
        IEnumerable<Chat> GetJoinedRooms(string userName);
        void UpdateDatabase();
        void AddToChat(string userId, int chatId);
        
    }
}