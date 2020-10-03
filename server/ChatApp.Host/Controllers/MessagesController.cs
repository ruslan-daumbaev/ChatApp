using ChatApp.Host.Hubs;
using ChatApp.Services.Dto;
using ChatApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Host.Controllers
{
    [ApiController]
    [Route("api/v1/messages")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesService messagesService;
        private readonly IHubContext<ChatHub, IChatHub> hubContext;

        public MessagesController(IHubContext<ChatHub, IChatHub> hubContext, IMessagesService messagesService)
        {
            this.messagesService = messagesService;
            this.hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages([FromQuery] MessagesRequestDto request, CancellationToken token)
        {
            var messages = await messagesService.GetMessages(request.PageSize, request.AnchorMessage, token);
            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] NewMessageDto message, CancellationToken token)
        {
            if (message == null || message.IsEmpty)
            {
                return BadRequest();
            }
            var newMessage = await messagesService.SaveMessage(message.User, message.MessageText, token);
            await hubContext.Clients.All.Notify(newMessage);
            return Ok(newMessage);
        }
    }
}
