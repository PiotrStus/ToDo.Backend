using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Interfaces;
using ToDo.Application.Logic.Abstractions;

namespace ToDo.Application.Logic.Item
{
    public static class GetAllItemsQuery
    {

        public class Request() : IRequest<Result>
        {

        }

        public class Result()
        {
            public required List<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();

            public class ToDoItem()
            {
                public required int Id { get; set; }
                public required string Title { get; set; }
                public string? Description { get; set; }
                public DateTimeOffset DueDate { get; set; }

            }
        }

        public class Handler : BaseQueryHandler, IRequestHandler<Request, Result>
        {
            public Handler(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
            {
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                var items = await _applicationDbContext.Items
                     .Select(c => new Result.ToDoItem()
                     {
                         Id = c.Id,
                         Description = c.Description,
                         Title = c.Title,
                         DueDate = c.DueDate,
                     })
                     .ToListAsync();

                return new Result()
                {
                    ToDoItems = items
                };
            }
        }
    }
}