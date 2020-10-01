using System;

namespace ChatApp.Services.Dto
{
    public class GetMessageDto
    {
        public Guid Id { get; set; }

        public string User { get; set; }

        public string MessageText { get; set; }

        public DateTime InsertDate { get; set; }
    }
}