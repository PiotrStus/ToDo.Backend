﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Common
{
    public abstract class DomainEntity
    {
        public int Id { get; set; }
    }
}
