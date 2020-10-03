using ChatApp.Data.Configurations;
using ChatApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
        }
    }
}