using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.Services.Dto;
using JetBrains.Annotations;

namespace ChatApp.Services.Interfaces
{
    public interface IMessagesService
    {
        Task<IReadOnlyList<GetMessageDto>> GetMessages();

        Task<GetMessageDto> SaveMessage([NotNull] string user, [NotNull] string messageText, CancellationToken token);
    }
}