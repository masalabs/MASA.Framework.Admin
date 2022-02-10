namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class UpdateBlogTypeModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
    }
}
