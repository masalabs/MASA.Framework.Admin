namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class BlogInfoListViewModel
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
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 关联id
        /// </summary>
        public List<BlogLabelRelationsViewModel> Relations { get; set; }
    }
}
