using MASA.Framework.Admin.Blog.Data.Blog;
using MASA.Framework.Admin.Caller;
using MASA.Framework.Admin.Contracts.Blogs.BlogAdvertisingPictures.Enums;

namespace MASA.Framework.Admin.Blog.Pages.BlogFrontend
{
    public partial class Home
    {
        private int _page = 1;
        private int _pageCount = 1;
        private bool _showWrite = false;
        private string _label = string.Empty;
        private CreateBlogInfoModel _options = new() { State = StateTypes.Reviewed };
        private List<(Guid, string)> _typeList = new();

        private GetBlogArticleHomeOptions _searchOptions = new()
        {
            PageIndex = 1, PageSize = 20
        };

        public PagingResult<BlogInfoHomeListViewModel> Blogs { get; set; } =
            new PagingResult<BlogInfoHomeListViewModel>();

        public List<BlogAdvertisingPicturesListViewModel> Ad { get; set; } = new();


        protected override async Task OnInitializedAsync()
        {
            await FetchBlogs();
            //分类
            var typesResult = await BlogCaller.BlogTypeService.PagingAsync(new GetBlogTypePagingOption() { PageIndex = 1, PageSize = int.MaxValue });
            if (typesResult.Data is not null)
            {
                _typeList = typesResult.Data.Select(m => (m.Id, m.TypeName)).ToList();
            }
            
            _typeList.Insert(0,(Guid.Empty, "全部"));
        }


        [Inject] protected BlogCaller BlogCaller { get; set; }

        private async Task SelectedChipChanged(StringNumber id)
        {
            var guid = Guid.Parse(id.ToString());

            _page= 1;
            _searchOptions.TypeId = guid;

            await FetchBlogs();
        }

        private async Task FetchBlogs()
        {
            _searchOptions.PageIndex = _page;
            
            Blogs = await BlogCaller.ArticleService.BlogArticleHomeAsync(_searchOptions);
            if (Blogs.TotalCount > 0)
            {
                _pageCount = Convert.ToInt32(Math.Ceiling((Decimal)Blogs.TotalCount / Convert.ToDecimal(Blogs.Size)));
            }
        }

        protected override async void OnInitialized()
        {
            //分类
            var typesResult = await BlogCaller.BlogTypeService.PagingAsync(new GetBlogTypePagingOption()
            { PageIndex = 1, PageSize = 50 });
            if (typesResult.Data is not null)
            {
                _typeList = typesResult.Data.Select(m => (m.Id, m.TypeName)).ToList();
            }

            await this.GetAdAsync();
        }

        private void ToWrite()
        {
            _showWrite = true;
        }

        public async Task SubmitBlog()
        {
            await BlogCaller.ArticleService.CreateAsync(_options);
            _showWrite = false;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <returns></returns>
        private async Task Cancel()
        {
            _showWrite = false;
        }

        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="ex"></param>
        private void AddLabel(MouseEventArgs ex)
        {
            _options.Labels ??= new();
            _options.Labels.Add(_label);
            _label = string.Empty;
        }

        /// <summary>
        /// 获取首页广告位
        /// </summary>
        private async Task GetAdAsync()
        {
            Ad = await BlogCaller.AdvertisingPicturesService.GetList(new()
            {
                Types = new()
                {
                    BlogAdvertisingPicturesTypes.Home,
                    BlogAdvertisingPicturesTypes.HomeLowerRight
                }
            });
        }
    }
}