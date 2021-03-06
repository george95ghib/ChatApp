using System.Collections.Generic;
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

        public void CreateRoom(string roomName, string userId)
        {
            var user = GetUser(userId);

            var room = new Chat();
            
            room.Name = roomName;
            room.Users = new List<User>();
            room.Users.Add(user);
            room.Type = ChatType.Room;

            _context.Chats.Add(room);
            _context.SaveChanges();

        }

        public string GetUserId(string userName)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == userName).Id;
        }

        public User GetUser(string userId)
        {
            return _context.Users.FirstOrDefault(x => x.Id == userId);
        }

        public Chat GetChat(int id)
        {
            return _context.Chats.Include(m => m.Messages).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Chat> GetAllRooms()
        {
            var rooms = _context.Chats.Where(x => x.Type == ChatType.Room).ToList();

            return rooms;
        }
    }
}