using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.Services.Dto;
using JetBrains.Annotations;

namespace ChatApp.Services.Interfaces
{
    public interface IMessagesService
    {
        Task<IReadOnlyList<MessageDto>> GetMessages(int pageSize, int? anchorMessage, CancellationToken token);

        Task<MessageDto> SaveMessage([NotNull] string user, [NotNull] string messageText, CancellationToken token);
    }
}