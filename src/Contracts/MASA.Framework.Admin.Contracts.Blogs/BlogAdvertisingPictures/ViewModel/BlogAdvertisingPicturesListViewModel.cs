namespace MASA.Framework.Admin.Contracts.Blogs.BlogAdvertisingPictures.ViewModel
{
    public class BlogAdvertisingPicturesListViewModel: ViewModelBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Pic { get; set; }

        /// <summary>
        /// 类型：1 首页 2文章列表
        /// </summary>
        public short Type { get; set; }

        /// <summary>
        /// 位置：1 列表头部
        /// </summary>
        public short Location { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 状态：true启用 false停用
        /// </summary>
        public bool Status { get; set; }
    }
}
