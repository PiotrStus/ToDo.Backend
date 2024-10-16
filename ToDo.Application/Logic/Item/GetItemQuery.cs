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
    public static class GetItemQuery
    {
        public class Request :IRequest<Result>
        {
            public int Id { get; set; }
        }

        public class Result 
        {
            public required int Id { get; set; }
            public required string Title { get; set; }
            public string? Description { get; set; }
            public DateTimeOffset DueDate { get; set; }
        }


        public class Handler : BaseQueryHandler, IRequestHandler<Request, Result>
        {
            public Handler(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
            {
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {

                var model = await _applicationDbContext.Items.FirstOrDefaultAsync(c => c.Id == request.Id);

                if (model == null)
                {
                    throw new ErrorException("ItemNotFound");
                }

                return new Result
                {
                        Id = request.Id,
                        Title = model.Title,
                        Description = model.Description,
                        DueDate = model.DueDate,
                };
            }
        }
    }
}