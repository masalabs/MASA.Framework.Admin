using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Caller.Model
{
    /// <summary>
    /// 分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingResult<T>
    {
        /// <summary>
        ///当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 当前页数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public long PageCount { get; set; }

        public PagingResult()
        {

        }

        public PagingResult(int pageIndex, int pageSize, long count, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            Data = data.ToList();
            PageCount = (TotalCount % PageSize) == 0 ? (TotalCount / PageSize) : (TotalCount / PageSize) + 1;
        }
    }
}
