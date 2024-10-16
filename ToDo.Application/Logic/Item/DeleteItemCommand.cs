using ToDo.Application.Exceptions;
using ToDo.Application.Interfaces;
using ToDo.Application.Logic.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Logic.Item
{
    public static class DeleteItemCommand
    {
        public class Request : IRequest<Result>
        {
            public required int Id { get; set; }
        }

        public class Result
        {

        }

        public class Handler : BaseCommandHandler, IRequestHandler<Request, Result>
        {
            public Handler(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
            {
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {

                var model = await _applicationDbContext.Items.FirstOrDefaultAsync(c => c.Id == request.Id);

                if (model == null)
                {
                    throw new ErrorException("ItemNotExists");
                }

                _applicationDbContext.Items.Remove(model);

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new Result();
            }
        }
    }
}
