namespace MASA.Framework.Admin.Service.Blogs.Model.BlogAdvertisingPictures.Model
{
    public class UpdateStatusBlogAdvertisingPicturesModel
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 状态：1启用 2 停用
        /// </summary>
        public bool Status { get; set; }
    }
}
