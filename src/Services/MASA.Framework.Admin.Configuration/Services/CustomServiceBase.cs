namespace MASA.Framework.Admin.Configuration.Services;

public class CustomServiceBase : ServiceBase
{
    public CustomServiceBase(IServiceCollection services) : base(services)
    {
    }

    protected ApiResultResponse<T> Success<T>(T data)
    {
        return new ApiResultResponse<T>(data);
    }
}
