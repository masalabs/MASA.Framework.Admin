namespace MASA.Framework.Sdks.Authentication;

public interface IClient
{
    Task<ApiResultResponse<bool>> VerifiedAsync(Guid roleId, string resource, string scope, string action);

    Task<ApiResultResponse<List<MenuInfoResponse>>> GetMenu(Guid roleId);
}
