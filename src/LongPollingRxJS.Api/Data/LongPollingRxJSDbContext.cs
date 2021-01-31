using LongPollingRxJS.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LongPollingRxJS.Api.Data
{
    public class LongPollingRxJSDbContext: DbContext, ILongPollingRxJSDbContext
    {
        public LongPollingRxJSDbContext(DbContextOptions options)
            :base(options) { }

        public static readonly ILoggerFactory ConsoleLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<ToDo> ToDos { get; private set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LongPollingRxJSDbContext).Assembly);
        }

        public ILongPollingRxJSDbContext AsNoTracking()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return this;
        }
    }
}
