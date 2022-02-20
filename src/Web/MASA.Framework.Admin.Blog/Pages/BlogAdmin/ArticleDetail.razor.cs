using MASA.Framework.Data.Mapping;
using Microsoft.JSInterop;

namespace MASA.Framework.Admin.Blog.Pages.BlogAdmin
{
    public partial class ArticleDetail
    {
        [Parameter]
        public Guid BlogInfoId { get; set; }

        [Inject]
        protected NavigationManager? Navigation { get; set; }

        [Inject] protected BlogCaller BlogCaller { get; set; }

        private BlogInfoListViewModel _blogInfo = new();
        private bool _showWrite = false;
        private UpdateBlogInfoModel _updateBlogInfoModel = new();

        [Inject] public IJSRuntime JsRuntime { get; set; }

        private CreateBlogReportModel _createBlogReportModel = new();
        private readonly Guid CurrentUserId = new Guid("DB4A41B4-0EE3-4957-A193-4DD4E633A52A");


        protected async override Task OnInitializedAsync()
        {
            await GetAsync();

            await base.OnInitializedAsync();
        }

        private async Task GetAsync()
        {
            if (BlogInfoId != Guid.Empty)
                _blogInfo = await BlogCaller.ArticleService.GetAsync(BlogInfoId);
        }

        private void ToApprove()
        {

        }

        /// <summary>
        /// 审核/上架
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task AuditArticleAsync(BlogInfoListViewModel model)
        {
            _updateBlogInfoModel =
                  new Mapping<BlogInfoListViewModel, UpdateBlogInfoModel>().Map(model);

            _updateBlogInfoModel.State = StateTypes.Reviewed;

            await BlogCaller.ArticleService.UpdateAsync(_updateBlogInfoModel);

            await PopupService.MessageAsync("审核成功", AlertTypes.Success);

            HrefArticlePage();
        }

        private void HrefArticlePage()
        {
            Navigation?.NavigateTo($"/blog-admin/article");
        }

        private async Task ScrollTop()
        {
            await JsRuntime.InvokeAsync<string>("scroll_top");
        }

        private void ToReport()
        {
            _showWrite = true;
            _createBlogReportModel.Title = _blogInfo.Title;
            _createBlogReportModel.BlogInfoId = BlogInfoId;
            _createBlogReportModel.CreatorUserId = CurrentUserId;
            _createBlogReportModel.Connect = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
        }

        /// <summary>
        /// 提交举报
        /// </summary>
        /// <returns></returns>
        public async Task SubmitReport()
        {
            await BlogCaller.ReportService.CreateAsync(_createBlogReportModel);
            _showWrite = false;

            await PopupService.MessageAsync("举报成功", AlertTypes.Success);
        }
    }
}
