using System.Threading.Tasks;
using ChatApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        private readonly IChatAppRepo _sqlChatApp;

        public RoomViewComponent(IChatAppRepo sqlChatApp)
        {
            _sqlChatApp = sqlChatApp;
        }

        public IViewComponentResult Invoke()
        {
            return View(_sqlChatApp.GetJoinedRooms(User.Identity.Name));
        }
    }
}