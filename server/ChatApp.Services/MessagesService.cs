using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.Data;
using ChatApp.Data.Models;
using ChatApp.Services.Dto;
using ChatApp.Services.Interfaces;
using MongoDB.Driver;

namespace ChatApp.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly ChatContext context;

        public MessagesService(ChatContext context)
        {
            this.context = context;
        }


        public async Task<IReadOnlyList<GetMessageDto>> GetMessages()
        {
            var messages = await context.Messages.Find(x => true).SortBy(x => x.InsertDate).ToListAsync();
            return messages.Select(x => new GetMessageDto
            {
                Id = x.Id,
                InsertDate = x.InsertDate,
                MessageText = x.MessageText,
                User = x.User
            }).ToList();
        }

        public async Task<GetMessageDto> SaveMessage(string user, string messageText, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                throw new ArgumentNullException(nameof(user));
            }
            var message = new Message
            {
                Id = Guid.NewGuid(),
                InsertDate = DateTime.UtcNow,
                MessageText = messageText,
                User = user
            };

            await context.Messages.InsertOneAsync(message, new InsertOneOptions(), token);
            return new GetMessageDto
            {
                Id = message.Id,
                InsertDate = message.InsertDate,
                MessageText = message.MessageText,
                User = message.User
            };
        }
    }
}
