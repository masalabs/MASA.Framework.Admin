namespace MASA.Framework.Admin.Contracts.Blogs
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
