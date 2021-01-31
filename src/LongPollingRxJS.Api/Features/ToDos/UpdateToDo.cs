using FluentValidation;
using LongPollingRxJS.Api.Data;
using LongPollingRxJS.Api.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LongPollingRxJS.Api.Features
{
    public class UpdateToDo
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ToDo).NotNull();
                RuleFor(request => request.ToDo).SetValidator(new ToDoValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public ToDoDto ToDo { get; set; }
        }

        public class Response
        {
            public ToDoDto ToDo { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ILongPollingRxJSDbContext _context;

            public Handler(ILongPollingRxJSDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var toDo = await _context.ToDos.FindAsync(request.ToDo.ToDoId);

                toDo.HtmlBody = request.ToDo.HtmlBody;
                toDo.Name = request.ToDo.Name;
                toDo.Completed = request.ToDo.Completed;
                toDo.Modified = DateTime.UtcNow;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    ToDo = toDo.ToDto()
                };
            }
        }
    }
}
