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
            return _context.Chats.Where(x => (x.Type == ChatType.Room) && (x.Id == id)).Include(u => u.Users).Include(m => m.Messages).FirstOrDefault();
        }

        public IEnumerable<Chat> GetAllRooms()
        {
            var rooms = _context.Chats.Where(x => x.Type == ChatType.Room).Include(u => u.Users).ToList();
            return rooms;
        }

        public void UpdateDatabase()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Chat> GetNotJoinedRooms(string userName)
        {
            // Get all rooms where the logged user didn't joined
            var user = GetUser(GetUserId(userName));
            var notJoinedRooms = _context.Chats
                                    .Include(u => u.Users)
                                    .Where(x => !x.Users.Contains(user))
                                    .ToList();
            return notJoinedRooms;
        }

        public IEnumerable<Chat> GetJoinedRooms(string userName)
        {
            // Get all rooms where the logged user joined
            var user = GetUser(GetUserId(userName));
            var joinedRooms = _context.Chats
                                    .Include(u => u.Users)
                                    .Where(x => x.Users.Contains(user))
                                    .ToList();
            return joinedRooms;
        }

        public void AddToChat(string userId, int chatId)
        {
            var chat = _context.Chats.FirstOrDefault(c => c.Id == chatId);
            chat.Users.Add(GetUser(userId));
        }
    }
}