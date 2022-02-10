using MASA.Utils.Caller.HttpClient;

namespace MASA.Framework.Admin.Caller.Callers;

public class BlogCaller : HttpClientCallerBase
{
    public BlogCaller(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override string BaseAddress { get; set; } = "http://localhost:18500";
}