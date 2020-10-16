using System;

namespace ChatApp.Data.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string MessageText { get; set; }

        public DateTime InsertDate { get; set; }
    }
}