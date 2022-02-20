namespace MASA.Framework.Admin.Contracts.Base.Response;

public class ApiResultResponse<TEntity> : ApiResultResponseBase
{
    public TEntity? Data { get; set; }

    private ApiResultResponse() : base(Const.Code.SUCCESS, "")
    {
    }

    public ApiResultResponse(TEntity? data) : this()
    {
        Data = data;
    }
}
