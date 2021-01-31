using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LongPollingRxJS.Api.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class ToDo
    {
        [Key]
        public Guid ToDoId { get; set; }

        public string Name { get; set; }
        public string HtmlBody { get; set; }
        public DateTime? Deleted { get; set; }
        public DateTime? Completed { get; set; }
    }

}
