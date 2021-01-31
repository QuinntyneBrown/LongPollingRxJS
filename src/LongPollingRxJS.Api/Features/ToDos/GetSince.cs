using LongPollingRxJS.Api.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LongPollingRxJS.Api.Features
{
    public class GetSince
    {
        public class Request : IRequest<Response> {
            public DateTime? Since { get; set; }
        }

        public class Response
        {
            public List<ToDoDto> ToDos { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ILongPollingRxJSDbContext _context;

            public Handler(ILongPollingRxJSDbContext context){
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
			    return new Response() { 
                    ToDos = request.Since.HasValue 
                    ? await _context.AsNoTracking().ToDos.Where(x => x.Modified> request.Since).Select(x =>x.ToDto()).ToListAsync()
                    : await _context.AsNoTracking().ToDos.Select(x => x.ToDto()).ToListAsync()
                };
            }
        }
    }
}
