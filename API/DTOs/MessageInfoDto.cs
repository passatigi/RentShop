namespace API.DTOs
{
    public class MessageInfoDto
    {
        public int RecipientId { get; set; }
        public int OrderId { get; set; }
        public string Content { get; set; }

        public int Page { get; set; }
    }
}