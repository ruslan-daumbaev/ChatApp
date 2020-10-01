using ChatApp.Data.Configuration;
using ChatApp.Data.Models;
using MongoDB.Driver;

namespace ChatApp.Data
{
    public class ChatContext
    {
        public ChatContext(IChatDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Messages = database.GetCollection<Message>(settings.ChatCollectionName);
        }

        public IMongoCollection<Message> Messages { get; }
    }
}