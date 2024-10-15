using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Common;

namespace ToDo.Domain.Entities
{
    public class Items : DomainEntity
    {
        public required string Title { get; set; }

        public string? Description { get; set; }

        public required DateTimeOffset DueDate { get; set; }

        public required bool IsCompleted { get; set; }

        public required DateTimeOffset CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
