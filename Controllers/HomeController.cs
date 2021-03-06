using System;
using System.Collections.Generic;
using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        // GET requested chat
        [Authorize]
        [HttpGet]
        public IActionResult Index(int id)
        {
            if(id == 0)
            {
                id = 1;
            }

            var chat = _homeService.GetChat(id);

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

        [HttpGet]
        public IActionResult Chat(int id)
        {
            if(id == 0)
            {
                id = 1;
            }

            var chat = _homeService.GetChat(id);

            return View(chat);
        }

        [HttpPost]
        public IActionResult SendMessage(int chatId, string message)
        {
            return View();
        }
    }
}