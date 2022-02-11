using MASA.Framework.Admin.Caller;

namespace MASA.Framework.Admin.Blog.Pages.BlogFrontend;

/// <summary>
/// 新建、编辑博文
/// </summary>
public partial class Write: BlogFrontComponentBase
{
    private bool _prevShow;
    private CreateBlogInfoModel _options = new() { State = StateTypes.Reviewed };
    private List<(Guid, string)> _typeList = new();

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public Guid BlogId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public bool Show { get; set; }

    [Inject]
    protected BlogCaller BlogCaller { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public EventCallback<bool> ShowChanged { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected async override Task OnInitializedAsync()
    {
        //分类
        var typesResult = await BlogCaller.BlogTypeService.PagingAsync(new GetBlogTypePagingOption() { PageIndex =1, PageSize=int.MaxValue});
        if(typesResult.Data is not null)
        {
            _typeList = typesResult.Data.Select(m => (m.Id, m.TypeName)).ToList();
        }
        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (_prevShow != Show && Show)
        {
            _prevShow = true;

            //详情
        }
    }

    private async void SubmitBlog()
    { 
    
    }

    /// <summary>
    /// 取消
    /// </summary>
    /// <returns></returns>
    private async Task Cancel()
    {
        Show = false;
        await ShowChanged.InvokeAsync(Show);
    }
}