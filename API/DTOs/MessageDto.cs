using System;

namespace API.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }

        public int SenderId { get; set; }
        
        public int RecipientId { get; set; }

        public int OrderId { get; set; }

        public string Content { get; set; }

        public bool isRead  { get; set; } = false;
        public DateTime MessageSent  { get; set; } 

    }
}