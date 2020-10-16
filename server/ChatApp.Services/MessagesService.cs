using ChatApp.Data;
using ChatApp.Data.Models;
using ChatApp.Services.Dto;
using ChatApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly ChatContext context;

        public MessagesService(ChatContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyList<MessageDto>> GetMessages(int pageSize, int? anchorMessage, CancellationToken token)
        {
            var query = context.Messages.OrderByDescending(x => x.Id).AsQueryable();
            if (anchorMessage.HasValue)
            {
                query = query.Where(x => x.Id < anchorMessage);
            }
            var messages = await query.Take(pageSize).ToListAsync(token);
            return messages.Select(x => new MessageDto
            {
                Id = x.Id,
                MessageText = x.MessageText,
                User = x.UserName
            }).OrderBy(x => x.Id).ToList();
        }

        public async Task<MessageDto> SaveMessage(string user, string messageText, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                throw new ArgumentNullException(nameof(user));
            }
            var message = new Message
            {
                InsertDate = DateTime.UtcNow,
                MessageText = messageText,
                UserName = user
            };

            context.Add(message);
            await context.SaveChangesAsync(token);

            return new MessageDto
            {
                Id = message.Id,
                MessageText = message.MessageText,
                User = message.UserName
            };
        }
    }
}
