using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        public RoomViewComponent()
        {
            // to be implemented
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}