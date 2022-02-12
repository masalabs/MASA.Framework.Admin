using MASA.Framework.Admin.Caller;
using MASA.Framework.Admin.Contracts.Blogs.BlogAdvertisingPictures.Enums;

namespace MASA.Framework.Admin.Blog.Pages.BlogFrontend
{
    public partial class BlogInfo : BlogFrontComponentBase
    {
        private int _page = 1;
        private int _pageCount = 1;
        private bool _showWrite = false;
        private readonly Guid CurrentUserId = new Guid("DB4A41B4-0EE3-4957-A193-4DD4E633A52A");
        private BlogInfoListViewModel _blogInfo = new();

        [Parameter]
        public Guid BlogInfoId { get; set; }

        [Inject] protected BlogCaller BlogCaller { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //详情
            await GetAsync();
            
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                await BlogCaller.ArticleService.AddVisitsAsync(new AddBlogVisitModel() { BlogId = BlogInfoId });
            }
        }

        private async Task GetAsync()
        {
            if(BlogInfoId != Guid.Empty)
                _blogInfo = await BlogCaller.ArticleService.GetAsync(BlogInfoId);
        }

        private void ToReport()
        {
            _showWrite = true;
        }

        private void ToApprove()
        {

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