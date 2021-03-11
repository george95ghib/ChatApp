using System;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Dtos
{
    public class ReceiveMessageDto
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
    }
}