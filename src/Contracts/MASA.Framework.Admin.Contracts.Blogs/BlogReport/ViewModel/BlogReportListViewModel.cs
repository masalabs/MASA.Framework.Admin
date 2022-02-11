using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.Blogs.BlogReport.ViewModel
{
    public class BlogReportListViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 举报标题
        /// </summary>
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 举报链接
        /// </summary>
        [StringLength(500)]
        public string Connect { get; set; } = string.Empty;

        /// <summary>
        /// 文章编号
        /// </summary>
        [StringLength(36)]
        public Guid BlogInfoId { get; set; }

        /// <summary>
        /// 文章名称
        /// </summary>
        public string BlogInfoName { get; set; }

        /// <summary>
        /// 举报详细信息
        /// </summary>
        [StringLength(500)]
        public string Detail { get; set; } = string.Empty;

        /// <summary>
        /// 举报理由
        /// </summary>
        public ReasonTypes Reason { get; set; }

        /// <summary>
        /// CreatorUserId
        /// </summary>
        [StringLength(36)]
        public Guid CreatorUserId { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
