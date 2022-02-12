namespace MASA.Framework.Admin.Contracts.Blogs.BlogReport.Options
{
    public class GetBlogReportOptions : PagingOptions
    {
        public string Title { get; set; }

        public ReasonTypes? ReaosnType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
