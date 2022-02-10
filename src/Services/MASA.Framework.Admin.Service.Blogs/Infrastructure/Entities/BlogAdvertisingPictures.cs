using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Service.Blogs.Infrastructure.Entities
{
    /// <summary>
    /// 广告图片
    /// </summary>
    public class BlogAdvertisingPictures : EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Pic { get; set; }
    }
}
