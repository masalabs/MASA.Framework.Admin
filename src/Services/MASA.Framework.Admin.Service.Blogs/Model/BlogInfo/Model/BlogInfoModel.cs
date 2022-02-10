using MASA.Framework.Admin.Service.Blogs.Domain.Entities;
using MASA.Framework.Admin.Service.Blogs.Infrastructure.Enum;

namespace MASA.Framework.Admin.Service.Blogs.Model.BlogInfo.Model
{
    public class BlogInfoModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 文章状态
        /// </summary>
        public StateTypes state { get; set; }

        /// <summary>
        /// 类型编号
        /// </summary>
        public Guid typeId { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 访问数量
        /// </summary>
        public int visits { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int commentCount { get; set; }

        /// <summary>
        /// 是否展示
        /// </summary>
        public bool isShow { get; set; } = true;

        /// <summary>
        /// 点赞数量
        /// </summary>
        public int approvedCount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BlogType blogType { get; set; } = default!;

        public static BlogInfoModel FromOrder(Blogs.Domain.Entities.BlogInfo blogInfo)
        {
            return new BlogInfoModel
            {
                title = blogInfo.Title,
                state = blogInfo.State,
                typeName = blogInfo.BlogType.TypeName,
                content = blogInfo.Content,
                visits = blogInfo.Visits,
                commentCount = blogInfo.CommentCount,
                approvedCount = blogInfo.ApprovedCount,
                remark = blogInfo.Remark,
            };
        }
    }
}
