using System;

namespace API.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public int SenderId { get; set; }
        public AppUser Sender { get; set; }
        public int RecipientId { get; set; }
        public AppUser Recipient { get; set; }

        public string Content { get; set; }
        public bool isRead  { get; set; }
        public DateTime MessageSent  { get; set; } = DateTime.UtcNow;

        public int OrderId { get; set; }
        public Order Order { get; set; }

        // public bool SenderDeleted { get; set; }
        // public bool RecipientDeleted { get; set; }
    }
}