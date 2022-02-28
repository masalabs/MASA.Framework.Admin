using Masa.Utils.Exceptions;
using System.Net;

namespace Masa.Framework.Sdks.Authentication.Callers;

public abstract class CallerBase : HttpClientCallerBase
{
    protected CallerBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<ApiResultResponse<TResponse>> ResultAsync<TResponse>(Func<Task<TResponse>> func)
    {
        try
        {
            var response = await func.Invoke();
            return ApiResultResponse<TResponse>.ResponseSuccess(response, "success");
        }
        catch (Exception ex)
        {
            if (ex is UserFriendlyException) return ApiResultResponse<TResponse>.ResponseLose(ex.Message, default);
            else return ApiResultResponse<TResponse>.ResponseLose("The service is abnormal, please contact the administrator!", default);
        }
    }

    protected async Task<ApiResultResponseBase> ResultAsync(Func<Task<HttpResponseMessage>> func)
    {
        try
        {
            var response =await func.Invoke();
            return response.StatusCode switch
            {
                HttpStatusCode.OK or HttpStatusCode.Accepted or HttpStatusCode.NoContent => ApiResultResponseBase.ResponseSuccess("success"),
                (HttpStatusCode)MasaHttpStatusCode.UserFriendlyException => ApiResultResponseBase.ResponseLose(await response.Content.ReadAsStringAsync()),
                _ => ApiResultResponseBase.ResponseSuccess("The service is abnormal, please contact the administrator!"),
            };
        }
        catch (Exception ex)
        {
            return ApiResultResponseBase.ResponseLose("The service is abnormal, please contact the administrator!");
        }
    }
}
