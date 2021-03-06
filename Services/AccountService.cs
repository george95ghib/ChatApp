using System.Threading.Tasks;
using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Services
{
    public class AccountService : Controller
    {
        
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<int> RegisterUser(string username, string password)
        {

            var user = new User
            {
                UserName = username
            };

            var result = await _userManager.CreateAsync(user, password);

            if(result.Succeeded)
            {
                return 1;
            }

            return 0;
        }

        public async Task<User> LoginUser(string username, string password)
        {
            var user = new User
            {
                UserName = username
            };

            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);

            if(result.Succeeded)
            {
                return user;
            }

            return null;
        }
    }
}