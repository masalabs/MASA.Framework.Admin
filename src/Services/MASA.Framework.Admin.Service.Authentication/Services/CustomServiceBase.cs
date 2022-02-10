namespace MASA.Framework.Admin.Service.Authentication.Services;

public class CustomServiceBase : ServiceBase
{
    public CustomServiceBase(IServiceCollection services) : base(services)
    {
    }

    protected ApiResultResponse<T> Success<T>(T data, string message = "success")
    {
        return new ApiResultResponse<T>(data)
        {
            Message = message
        };
    }

    protected ApiResultResponseBase Success(string message = "success")
    {
        return new ApiResultResponseBase(Code.SUCCESS, message);
    }
}
