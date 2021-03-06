namespace Masa.Framework.Admin.Rcl.Rbac.Pages.User.ViewModel
{
    public class PaginationPage<T>
    {
        public List<T> PageData { get; set; } = new();

        public string? Name { get; set; }

        public bool Enabled { get; set; }

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public int PageCount => (int)Math.Ceiling(CurrentCount / (double)PageSize);

        public long CurrentCount { get; set; }
    }
}
