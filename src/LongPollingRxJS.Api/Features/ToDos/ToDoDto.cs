using System;

namespace LongPollingRxJS.Api.Features
{
    public class ToDoDto
    {
        public Guid? ToDoId { get; set; }
        public string Name { get; set; }
        public string HtmlBody { get; set; }
        public DateTime? Completed { get; set; }
    }
}
