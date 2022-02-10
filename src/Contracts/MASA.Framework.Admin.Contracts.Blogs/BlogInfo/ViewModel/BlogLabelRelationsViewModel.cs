namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class BlogLabelRelationsViewModel
    {
        /// <summary>
        /// 关联id
        /// </summary>
        public Guid RelationId { get; set; }

        /// <summary>
        /// 标签id
        /// </summary>
        public Guid LabelId { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string LabelName { get; set; }
    }
}
