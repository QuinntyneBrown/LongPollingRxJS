using LongPollingRxJS.Api.Data;
using LongPollingRxJS.Api.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LongPollingRxJS.Api.Features
{
    public class GetToDoById
    {
        public class Request : IRequest<Response> {  
            public Guid ToDoId { get; set; }        
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

                var toDo = await _context.AsNoTracking().ToDos.FindAsync(request.ToDoId);

                return new Response() { 
                    ToDo = toDo.ToDto()
                };
            }
        }
    }
}
