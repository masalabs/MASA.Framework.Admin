namespace MASA.Framework.Admin.Blog.Pages.BlogBackend.Components
{
    public partial class WithdrawModal
    {
        [Parameter]
        public bool Visible { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        [EditorRequired]
        [Parameter]
        public EventCallback<WithdrawBlogArticleModel> OnOk { get; set; }

        private WithdrawBlogArticleModel Data { get; set; } = new()
        {
            ReasonType = ReasonTypes.Other
        };

        private async Task HandleOnOk()
        {
            if (OnOk.HasDelegate)
            {
                await OnOk.InvokeAsync(Data);
            }
        }
    }
}
