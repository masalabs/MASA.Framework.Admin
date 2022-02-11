namespace MASA.Framework.Admin.Configuration.Infrastructure.Middleware.GlobalException;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    ///
    /// </summary>
    /// <param name="next"></param>
    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UserFriendlyException ex)
        {
            await SetResponseAsync(context, Code.FRIENDLY_HTTP_STATUSCODE, ex.Code, ex.Message);
        }
        catch (ValidationException ex)
        {
            await SetResponseAsync(context, Code.FRIENDLY_HTTP_STATUSCODE, Code.PARAMETER_ERROR, ex.Errors.Select(err => err.ToString()).FirstOrDefault()!);
        }
        catch (Exception ex)
        {
            await SetResponseAsync(context, Code.SYSTEM_ERROR_HTTP_STATUSCODE, Code.SYSTEM_ERROR, ex.Message);
        }
    }

    static async Task SetResponseAsync(
        HttpContext context,
        int statusCode,
        string code,
        string message)
    {
        context.Response.Clear();
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json; charset=utf-8";
        string response = System.Text.Json.JsonSerializer.Serialize(new ApiResultResponseBase(code, message));
        await context.Response.WriteAsync(response);
    }
}
