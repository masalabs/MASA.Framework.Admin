using MASA.Framework.Admin.Caller;
using MASA.Framework.Admin.Contracts.Blogs.BlogAdvertisingPictures.Enums;

namespace MASA.Framework.Admin.Blog.Pages.BlogFrontend
{
    public partial class BlogInfo : BlogFrontComponentBase
    {
        private int _page = 1;
        private int _pageCount = 1;
        private bool _showWrite = false;
        private string _reportComment = string.Empty;
        private bool _reportCommentLoading = false;
        private readonly Guid CurrentUserId = new Guid("DB4A41B4-0EE3-4957-A193-4DD4E633A52A");
        private BlogInfoListViewModel _blogInfo = new();
        private PagingResult<BlogCommentInfoListViewModel> _blogCommentInfoList = new();

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
            await GetCommentList();
        }
        /// <summary>
        /// 获取评论列表
        /// </summary>
        /// <returns></returns>
        private async Task GetCommentList()
        {
            _blogCommentInfoList = await BlogCaller.CommentsService.GetListAsync(new GetBlogCommentInfoOptions()
            {
                BlogInfoId = BlogInfoId,
                PageIndex = _page,
                PageSize = 10
            });
        }
        private async Task GetAsync()
        {
            if(BlogInfoId != Guid.Empty)
            {
                _blogInfo = await BlogCaller.ArticleService.GetAsync(BlogInfoId, CurrentUserId);
            }   
        }

        private void ToReport()
        {
            _showWrite = true;
        }
        /// <summary>
        /// 发表评论
        /// </summary>
        private async Task ToReportComment()
        {
            _reportCommentLoading = true;
            await BlogCaller.CommentsService.CreateAsync(new AddCommentModel()
            {
                BlogInfoId = BlogInfoId,
                TypeId = _blogInfo.typeId,
                CommentContent = _reportComment,
                CreatorUserId = CurrentUserId
            });
            //Message("评论发表成功", AlertTypes.Success);
            _reportCommentLoading = false;
            _reportComment = String.Empty;
            await GetCommentList();
        }
        private async Task ToApprove()
        {
            await BlogCaller.ArticleService.AddBlogApprovedRecordAsync(new BlogApprovedRecordModel()
            {
                BlogId = BlogInfoId,
                IsApproved = !_blogInfo.IsApproved,
                UserId = CurrentUserId
            });
            await GetAsync();
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