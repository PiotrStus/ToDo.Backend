using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Interfaces;

namespace ToDo.Application.Logic.Abstractions
{
    public class BaseCommandHandler
    {
        protected readonly IApplicationDbContext _applicationDbContext;

        public BaseCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
    }
}
