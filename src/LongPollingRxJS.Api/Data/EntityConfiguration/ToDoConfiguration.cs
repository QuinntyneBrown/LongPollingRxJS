using LongPollingRxJS.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LongPollingRxJS.Api.Data.EntityConfiguration
{
    public class ToDoConfiguration : IEntityTypeConfiguration<ToDo>
    {
        public void Configure(EntityTypeBuilder<ToDo> builder)
        {
            builder.HasQueryFilter(p => !p.Deleted.HasValue);
        }
    }
}
