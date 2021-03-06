using ChatApp.Models;

namespace ChatApp.Data
{
    public interface IChatAppRepo 
    {
        Chat GetChat(int id);
    }
}