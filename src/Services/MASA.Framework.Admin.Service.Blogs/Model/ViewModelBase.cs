namespace MASA.Framework.Admin.Service.Blogs.Model
{
    public class ViewModelBase
    {
        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// LastModificationTime
        /// </summary>
        public DateTime LastModificationTime { get; set; } = DateTime.Now;
    }
}
