﻿namespace Masa.Framework.Sdks.Authentication.Callers;

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
            return ApiResultResponse<TResponse>.ResponseLose(ex.Message, default);
        }
    }

    protected async Task<ApiResultResponseBase> ResultAsync(Func<Task> func)
    {
        try
        {
            await func.Invoke();
            return ApiResultResponseBase.ResponseSuccess("success");
        }
        catch (Exception ex)
        {
            return ApiResultResponseBase.ResponseLose(ex.Message);
        }
    }
}
