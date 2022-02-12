namespace MASA.Framework.Admin.Contracts.Base.Response;

public class ApiResultResponseBase
{
    public ApiResultResponseBase(string code, string message = "") : this()
    {
        Code = code;
        Success = Code == Const.Code.SUCCESS;
        Message = message;
    }

    public string Code { get; set; }

    public bool Success { get; set; }

    public string Message { get; set; }
}

