namespace ChatApp.Data.Configuration
{
    public class ChatDatabaseSettings : IChatDatabaseSettings
    {
        public string ChatCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}