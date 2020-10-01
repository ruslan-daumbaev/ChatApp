using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.Services.Dto;

namespace ChatApp.Services.Interfaces
{
    public interface IMessagesService
    {
        Task<IReadOnlyList<GetMessageDto>> GetMessages();

        Task<GetMessageDto> SaveMessage(string user, string messageText, CancellationToken token);
    }
}