using MASA.Framework.Admin.Caller;
using MASA.Framework.Admin.Contracts.Blogs.BlogAdvertisingPictures.Enums;

namespace MASA.Framework.Admin.Blog.Pages.BlogFrontend
{
    public partial class BlogInfo : BlogFrontComponentBase
    {
        private int _page = 1;
        private int _pageCount = 1;
        private bool _showWrite = false;
        private BlogInfoListViewModel _blogInfo = new();

        [Parameter]
        public string BlogInfoId { get; set; }

        [Inject] protected BlogCaller BlogCaller { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //详情
            await GetAsync();
        }

        private async Task GetAsync()
        {
            if(!string.IsNullOrWhiteSpace(BlogInfoId))
                _blogInfo = await BlogCaller.ArticleService.GetAsync(new Guid(BlogInfoId));
        }

        private void ToWrite()
        {
            _showWrite = true;
        }

        public async Task SubmitBlog()
        {
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
        /// 获取首页广告位
        /// </summary>
        private async Task GetAdAsync()
        {
            await BlogCaller.AdvertisingPicturesService.GetList(new()
            {
                Types = new()
                {
                    BlogAdvertisingPicturesTypes.Details,
                    BlogAdvertisingPicturesTypes.DetailsLowerRight
                }
            });
        }
    }
}