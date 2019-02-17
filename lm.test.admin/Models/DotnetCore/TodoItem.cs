using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lm.test.admin.Models.DotnetCore
{
    public class TodoItem
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsCompleted { get; set; }
    }
}
