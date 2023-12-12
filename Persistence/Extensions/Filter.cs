using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Extensions
{
    public class Filter<T>
    {
        public bool Condition { get; set; }
        public Expression<Func<T, bool>> Expression { get; set; }
    }
}
