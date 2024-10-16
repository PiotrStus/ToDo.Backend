using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Interfaces;

namespace ToDo.Application.Logic.Abstractions
{
    public class BaseQueryHandler
    {
        protected readonly IApplicationDbContext _applicationDbContext;

        public BaseQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
    }
}
