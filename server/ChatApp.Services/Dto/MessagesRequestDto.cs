namespace ChatApp.Services.Dto
{
    public class MessagesRequestDto
    {
        public int PageSize { get; set; }

        public int? AnchorMessage { get; set; }
    }
}