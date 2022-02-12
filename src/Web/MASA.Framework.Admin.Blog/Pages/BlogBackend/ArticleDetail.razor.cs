using MASA.Framework.Data.Mapping;
using Microsoft.JSInterop;

namespace MASA.Framework.Admin.Blog.Pages.BlogBackend
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

        private void ToReport()
        {
            _showWrite = true;
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

            Message("审核成功", AlertTypes.Success);

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
    }
}
