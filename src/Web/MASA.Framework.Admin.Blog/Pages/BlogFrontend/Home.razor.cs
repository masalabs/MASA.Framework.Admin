using MASA.Framework.Admin.Blog.Data.Blog;
using MASA.Framework.Admin.Caller;

namespace MASA.Framework.Admin.Blog.Pages.BlogFrontend
{
    public partial class Home
    {
        private int _page = 1;
        private int _pageCount = 1;
        private bool _showWrite = false;
        public PagingResult<BlogInfoListViewModel> Blogs { get; set; } = new PagingResult<BlogInfoListViewModel>();


        protected override async Task OnInitializedAsync()
        {
            await FetchBlogs();
        }


        [Inject]
        protected BlogCaller BlogCaller { get; set; }


        private async Task FetchBlogs()
        {
            Blogs = await BlogCaller.ArticleService.GetList(new GetBlogArticleOptions()
            {
                PageIndex= _page,
                PageSize= 10
            });
            if (Blogs.TotalCount > 0)
            {
                _pageCount = Convert.ToInt32(Math.Ceiling((Decimal)Blogs.TotalCount / Convert.ToDecimal(Blogs.Size)));
            }
        
        }
        protected override async void OnInitialized()
        {
        }
        private void ToWrite()
        {
            _showWrite = true;
        }
    }
}
