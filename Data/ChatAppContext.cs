using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ChatApp.Data
{
    public class ChatAppContext : IdentityDbContext<User>
    {
        public ChatAppContext(DbContextOptions<ChatAppContext> opt) : base(opt)
        {

        }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        
    }
    
}