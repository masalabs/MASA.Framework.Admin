using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.Dictionary.Dic.Options
{
    public class DicPagingOptions : PagingOptions
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool? Enable { get; set; }

        /// <summary>
        /// 创建开始时间
        /// </summary>
        public DateTimeOffset CreateStartTime { get; set; }

        /// <summary>
        /// 创建结束时间
        /// </summary>
        public DateTimeOffset CreateEndTime { get; set; }
    }
}
