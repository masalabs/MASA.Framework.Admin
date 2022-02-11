using MASA.Framework.Admin.Blog.Data.Blog;
using MASA.Framework.Admin.Caller;

namespace MASA.Framework.Admin.Blog.Pages.BlogFrontend
{
    public partial class Home
    {
        private int _page = 1;
        public List<BlogViewModel> Blogs { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Blogs = new List<BlogViewModel>()
                {
                    new BlogViewModel()
                    {
                        Id = Guid.NewGuid(),
                        Title = "什么是 .NET MAUI？",
                        Content = "什么是 .NET MAUI？2021/10/15.NET 多平台应用 UI ( .NET MAUI) 是一个跨平台框架，用于使用 c # 和 XAML 创建本机移动应用和桌面应用。重要.Net MAUI) 的 .NET 多平台应用 UI ( 的文档正在构造。",
                        LastModificationTime = DateTime.Now,
                        Type = ".NET MAUI",
                        Author = "Test",
                        CommentCount = 15,
                        ApprovedCount = 20,
                        Visits = 50
                    },
                    new BlogViewModel()
                    {
                        Id = Guid.NewGuid(),
                        Title = "什么是 .NET MAUI？",
                        Content = "什么是 .NET MAUI？2021/10/15.NET 多平台应用 UI ( .NET MAUI) 是一个跨平台框架，用于使用 c # 和 XAML 创建本机移动应用和桌面应用。重要.Net MAUI) 的 .NET 多平台应用 UI ( 的文档正在构造。",
                        LastModificationTime = DateTime.Now,
                        Type = ".NET MAUI",
                         Author = "Test",
                         CommentCount = 15,
                        ApprovedCount = 20,
                        Visits = 50
                    }
                };
        }


        [Inject]
        protected BlogCaller BlogCaller { get; set; }

        protected override async void OnInitialized()
        {
            var res = await BlogCaller.ArticleService.GetList(new() { PageIndex = 1, PageSize = 10});
        }

    }
}
