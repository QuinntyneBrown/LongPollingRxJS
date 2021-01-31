using LongPollingRxJS.Api.Data;
using LongPollingRxJS.Api.Models;
using Microsoft.Extensions.Configuration;

namespace LongPollingRxJS.Core.Seeding
{
    public static class SeedData
    {
        public static void Seed(LongPollingRxJSDbContext context, IConfiguration configuration)
        {
            context.ToDos.AddRange(new ToDo[]
            {
                new ToDo
                {
                    Name = "Groceries",
                    HtmlBody = "<h1>Get Lots</h1>"
                }
            });

            context.SaveChanges();
        }
    }
}
