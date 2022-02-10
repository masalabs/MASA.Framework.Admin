namespace MASA.Framework.Admin.Blog.Data.Blog
{
    public class BlogViewModel
    {
        public Guid Id { get; set; }
        public DateTime LastModificationTime { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Author { get; set; }
        public int ApprovedCount { get; set; }
        public int CommentCount { get; set; }
        public int Visits { get; set; }
    }
}
