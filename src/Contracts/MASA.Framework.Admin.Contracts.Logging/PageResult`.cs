using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.Logging
{
    public class PageResult<T>
    {
        public PageResult(int count, IEnumerable<T> data)
        {
            Count = count;
            Data = data;
        }

        public int Count { get; }

        public IEnumerable<T> Data { get; }
    }
}
