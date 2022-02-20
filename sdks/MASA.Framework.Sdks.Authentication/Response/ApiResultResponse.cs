namespace MASA.Framework.Sdks.Authentication.Response;

public class ApiResultResponse<TEntity>
{
    public bool Success { get; }

    public string Message { get; }

    public TEntity? Data { get; }

    private ApiResultResponse(bool success, string message, TEntity? data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public static ApiResultResponse<TEntity> ResponseSuccess(TEntity? data, string message) => new(true, message, data);

    public static ApiResultResponse<TEntity> ResponseLose(string message, TEntity? data) => new(false, message, data);
}
