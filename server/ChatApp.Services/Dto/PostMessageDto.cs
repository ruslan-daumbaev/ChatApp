namespace ChatApp.Services.Dto
{
    public class PostMessageDto
    {
        public string User { get; set; }

        public string MessageText { get; set; }

        public bool IsEmpty => string.IsNullOrWhiteSpace(User) || string.IsNullOrWhiteSpace(MessageText);
    }
}