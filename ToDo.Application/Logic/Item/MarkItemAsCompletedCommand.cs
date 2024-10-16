using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Exceptions;
using ToDo.Application.Interfaces;
using ToDo.Application.Logic.Abstractions;

namespace ToDo.Application.Logic.Item
{
    public static class MarkItemAsCompletedCommand
    {
        public class Request : IRequest<Result>
        {
            public required int Id { get; set; }

        }

        public class Result
        {
            public required int ItemId { get; set; }
        }

        public class Handler : BaseCommandHandler, IRequestHandler<Request, Result>
        {
            public Handler(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
            {
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                var item = await _applicationDbContext.Items
                    .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

                if (item == null)
                {
                    throw new ErrorException("ItemNotFound");
                }

                var utcNow = DateTime.UtcNow;
                item.IsCompleted = true;
                item.UpdatedAt = DateTime.UtcNow;
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new Result()
                {
                    ItemId = item.Id
                };
            }
        }
    }
}
