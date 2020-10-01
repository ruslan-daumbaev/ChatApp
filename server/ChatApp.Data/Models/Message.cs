using System;

namespace ChatApp.Data.Models
{
    public class Message
    {
        public Guid Id { get; set; }

        public string User { get; set; }

        public string MessageText { get; set; }

        public DateTime InsertDate { get; set; }
    }
}