namespace MASA.Framework.Admin.Contracts.Base.Response;

public class ApiResultResponse<TEntity> : ApiResultResponseBase
{
    private ApiResultResponse() : base(Const.Code.SUCCESS, "")
    {
    }

    public ApiResultResponse(TEntity? data) : this()
    {
        Data = data;
    }

    public TEntity? Data { get; set; }
}
