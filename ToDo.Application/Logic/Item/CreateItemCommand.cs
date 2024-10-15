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
    public static class CreateItemCommand
    {
        public class Request : IRequest<Result>
        {
            public required string Title { get; set; }
            public string? Description { get; set; }
            public DateTimeOffset DueDate { get; set; }
        }

        public class Result
        {
            public required int ItemId { get; set; }
        }

        public class Handler : BaseCommandHandler, IRequestHandler<Request, Result>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public Handler(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                var itemExists = await _applicationDbContext.Items.AnyAsync(i => i.Title == request.Title);
                if (itemExists)
                {
                    throw new ErrorException("ItemAlreadyExists");
                }

                var utcNow = DateTime.UtcNow;

                var item = new Domain.Entities.Item
                {
                    Title = request.Title,
                    Description = request.Description,
                    DueDate = request.DueDate,
                    CreatedAt = utcNow
                };

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new Result()
                {
                    ItemId = item.Id
                };
            }
        }
    }
}
