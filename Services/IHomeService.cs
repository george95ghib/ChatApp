using ChatApp.Models;

namespace ChatApp.Services
{
    public interface IHomeService
    {
        Chat GetChat(int id);
        void CreateRoom(string roomName, string userName);
        void BuildMessage(int chatId, string message, string userName);
        void UpdateDatabase();
    }
}