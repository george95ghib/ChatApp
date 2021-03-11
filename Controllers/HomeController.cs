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

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_homeService.GetNotJoinedRooms(User.Identity.Name));
        }

        [HttpGet]
        public IActionResult AddToChat(int chatId)
        {
            _homeService.AddToChat(User.Identity.Name, chatId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Chat(int chatId)
        {
            return View(_homeService.GetChat(chatId));
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