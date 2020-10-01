using ChatApp.Host.Hubs;
using ChatApp.Services.Dto;
using ChatApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> logger;
        private readonly IMessagesService messagesService;
        private readonly IHubContext<ChatHub, IChatHub> hubContext;

        public MessagesController(ILogger<MessagesController> logger, IHubContext<ChatHub, IChatHub> hubContext, IMessagesService messagesService)
        {
            this.logger = logger;
            this.messagesService = messagesService;
            this.hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await messagesService.GetMessages();
            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] PostMessageDto message, CancellationToken token)
        {
            var newMessage = await messagesService.SaveMessage(message.User, message.MessageText, token);
            await hubContext.Clients.All.Notify(newMessage);
            return Ok(newMessage);
        }
    }
}
