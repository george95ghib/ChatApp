using System;
using System.Threading.Tasks;
using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            if(String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                return View();
            }

            var user = new User
            {
                UserName = username
            };

            var register = await _userManager.CreateAsync(user, password);


            if(register.Succeeded)
            {
                return RedirectToAction(nameof(Login));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if(String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                return View();
            }

            var user = new User
            {
                UserName = username
            };

            var login = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);

            if(login.Succeeded)
            {                
                return RedirectToAction("Index", "Home");
            }

            return View("Login");
            
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}