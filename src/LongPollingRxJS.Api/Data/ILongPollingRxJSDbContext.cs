using LongPollingRxJS.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace LongPollingRxJS.Api.Data
{
    public interface ILongPollingRxJSDbContext
    {
        DbSet<ToDo> ToDos { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        ILongPollingRxJSDbContext AsNoTracking();
    }
}
