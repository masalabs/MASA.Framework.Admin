using MASA.Framework.Admin.Blog.Data.Blog;
using MASA.Framework.Admin.Blog.Shared;
using MASA.Framework.Admin.Caller;
using MASA.Framework.Admin.Contracts.Blogs.BlogAdvertisingPictures.Enums;

namespace MASA.Framework.Admin.Blog.Pages.BlogFrontend
{
    public partial class UserInfo : BlogFrontComponentBase
    {
        [CascadingParameter] public BlogFrontLayout Layout { get; set; }

        private int _page = 1;
        private int _pageCount = 1;
        private bool _showWrite = false;
        private string _label = string.Empty;
        private List<(Guid, string)> _typeList = new();
        private GetBlogArticleUserOptions _searchOptions = new() 
        { 
            Author = Guid.Empty, 
            PageIndex = 1, 
            PageSize = 20 
        };

        public PagingResult<BlogInfoListViewModel> Blogs { get; set; } =
            new PagingResult<BlogInfoListViewModel>();

        public List<BlogAdvertisingPicturesListViewModel> Ad { get; set; } = new();

        private BlogInfoListViewModel _options { get; set; }

        private UpdateBlogInfoModel _updateOption { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (Layout != null)
            {
                Layout.SearchEvent += AppOnSearch;
            }

            await FetchBlogs();
            await GetAdAsync();
            //分类
            var typesResult = await BlogCaller.BlogTypeService.PagingAsync(new GetBlogTypePagingOption()
            { PageIndex = 1, PageSize = int.MaxValue });
            if (typesResult.Data is not null)
            {
                _typeList = typesResult.Data.Select(m => (m.Id, m.TypeName)).ToList();
            }
        }

        [Inject] protected BlogCaller BlogCaller { get; set; }

        [Inject] protected NavigationManager? Navigation { get; set; }

        public void HrefDetailPage(Guid id)
        {
            Navigation?.NavigateTo($"/blogs/info/{id}");
        }

        private async Task AppOnSearch()
        {
            _searchOptions.Title = Layout.SearchName;

            await FetchBlogs();

            StateHasChanged();
        }

        private async Task FetchBlogs()
        {
            Blogs = await BlogCaller.ArticleService.BlogArticleByUserAsync(_searchOptions);
            if (Blogs.TotalCount > 0)
            {
                _pageCount = Convert.ToInt32(Math.Ceiling((Decimal)Blogs.TotalCount / Convert.ToDecimal(Blogs.Size)));
            }
        }

        private async Task ToWrite(Guid id)
        {
            //详情
            _options = await BlogCaller.ArticleService.GetAsync(id);
            _showWrite = true;
        }

        private async Task ToDelete(BlogInfoListViewModel blog)
        {
            Confirm(
               title: "删除文章类型",
               content: $"您确认要删除文章：<<{blog.Title}>>吗？",
               onOk: async () =>
               {
                   Guid[] ids = { blog.Id };
                   await BlogCaller.ArticleService.RemoveAsync(ids);

                   Message("删除成功", AlertTypes.Success);

               }, AlertTypes.Warning);

            StateHasChanged();
        }

        public async Task SubmitBlog()
        {
            _updateOption.Id = _options.Id;
            _updateOption.State = _options.State == StateTypes.OffTheShelf ? 
                StateTypes.ToBeReviewed : _updateOption.State;
            _updateOption.DeleteRelationIds = new();
            _updateOption.Content = _options.Content;
            _updateOption.Title = _options.Title;
            _updateOption.IsShow = _options.IsShow;
            _updateOption.TypeId = _options.TypeId;
            await BlogCaller.ArticleService.UpdateAsync(_updateOption);
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
            _updateOption.AddLabels ??= new();
            _updateOption.AddLabels.Add(_label);
            _label = string.Empty;
        }

        private void OnCloseClick(BlogLabelRelationsViewModel relation)
        {
            _options.Relations.Remove(relation);
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