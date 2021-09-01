namespace API.DTOs
{
    public class NewMessageDto
    {
        public int RecipientId { get; set; }
        public int OrderId { get; set; }
        public string Content { get; set; }
    }
}