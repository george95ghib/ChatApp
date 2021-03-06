using ChatApp.Models;

namespace ChatApp.Data
{
    public interface IChatAppRepo 
    {
        Chat GetChat(int id);
        void CreateRoom(string roomName, string userId);
        string GetUserId(string userId);
        User GetUser(string userId);
    }
}