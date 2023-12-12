using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Wrapper
{
    public class PaginatedList<T> : Result
    {
        public PaginatedList()
        {
        }

        public PaginatedList(IQueryable<T> source, int count)
        {
            TotalCount = count;
            Rows = source;
        }

        public PaginatedList(ICollection<T> source, int count)
        {
            TotalCount = count;
            Rows = source;
        }

        public int TotalCount { get; }

        public IEnumerable<T> Rows { get; }

        public static PaginatedList<T> Default => new PaginatedList<T>(Array.Empty<T>(), 0);
    }
}
