using System.Linq;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data
{
    public class SqlChatAppRepo : IChatAppRepo
    {
        private readonly ChatAppContext _context;

        public SqlChatAppRepo(ChatAppContext context)
        {
            _context = context;
        }

        public Chat GetChat(int id)
        {
            return _context.Chats.Include(m => m.Messages).FirstOrDefault(x => x.Id == id);
        }
    }
}