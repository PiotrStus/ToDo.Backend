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
    public static class UpdateItemCommand
    {
        public class Request : IRequest<Result>
        {
            public required int Id { get; set; }
            public required string Title { get; set; }
            public string? Description { get; set; }
            public required DateTimeOffset DueDate { get; set; }
            public required bool IsCompleted { get; set; }
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
                    throw new ErrorException("Item not found");
                }

                var itemExists = await _applicationDbContext.Items
                    .AnyAsync(i => i.Title == request.Title && i.DueDate.Year == request.DueDate.Year && i.DueDate.Month == request.DueDate.Month && i.DueDate.Day == request.DueDate.Day && i.Id != request.Id, cancellationToken);

                if (itemExists)
                {
                    throw new ErrorException("ItemAlreadyExist");
                }

                var utcNow = DateTime.UtcNow;

                item.Title = request.Title;
                item.Description = request.Description;
                item.DueDate = request.DueDate;
                item.IsCompleted = request.IsCompleted;
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
