using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatApp.Hubs;
using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        private readonly IHubContext<ChatHub> _hubContext;

        public HomeController(IHomeService homeService, IHubContext<ChatHub> hubContext)
        {
            _homeService = homeService;
            _hubContext = hubContext;
        }

        // GET requested chat
        [HttpGet]
        public IActionResult Index(int id)
        {
            
            if (id == 0)
            {
                id = 1;
            }

            var chat = _homeService.GetChat(id);

            if(chat == null)
            {
                return NotFound();
            }

            return View(chat);
            
        }

        [HttpGet]
        public IActionResult CreateRoom()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateRoom(string roomName)
        {
            _homeService.CreateRoom(roomName, User.Identity.Name);
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(int chatId, string message)
        {

            _homeService.BuildMessage(chatId, message, User.Identity.Name);

            return NoContent();
        }
    }
}