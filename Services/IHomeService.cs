using ChatApp.Models;

namespace ChatApp.Services
{
    public interface IHomeService
    {
        Chat GetChat(int id);
    }
}