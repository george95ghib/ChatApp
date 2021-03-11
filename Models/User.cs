using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Models
{
    public class User : IdentityUser
    {
        public ICollection<Chat> Chats { get; set; }
    }
}