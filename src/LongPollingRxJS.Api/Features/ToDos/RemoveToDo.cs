using FluentValidation;
using LongPollingRxJS.Api.Data;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LongPollingRxJS.Api.Features
{
    public class RemoveToDo
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit> {  
            public Guid ToDoId { get; set; }
        }

        public class Response
        {
            public ToDoDto ToDo { get; set; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly ILongPollingRxJSDbContext _context;

            public Handler(ILongPollingRxJSDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken) {

                var toDo = await _context.AsNoTracking().ToDos.FindAsync(request.ToDoId);

                toDo.Deleted = DateTime.UtcNow;

                await _context.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
