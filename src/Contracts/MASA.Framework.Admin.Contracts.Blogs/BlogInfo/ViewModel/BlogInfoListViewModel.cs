namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class BlogInfoListViewModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文章状态
        /// </summary>
        public StateTypes State { get; set; }

        /// <summary>
        /// 类型编号
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 访问数量
        /// </summary>
        public int Visits { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// 是否展示
        /// </summary>
        public bool IsShow { get; set; } = true;

        /// <summary>
        /// 点赞数量
        /// </summary>
        public int ApprovedCount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime ReleaseTime { get; set; }

        /// <summary>
        /// 关联id
        /// </summary>
        public List<BlogLabelRelationsViewModel> Relations { get; set; }

        /// <summary>
        /// 是否已点赞
        /// </summary>
        public bool IsApproved { get; set; }
        
        /// <summary>
        /// 下架原因
        /// </summary>
        public string WithdrawReason { get; set; }
    }
}
