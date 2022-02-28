namespace Masa.Framework.Sdks.Authentication;

public class DefaultClient : IClient
{
    private const string Success = "success";

    private readonly Callers.AuthenticationCaller _authenticationCaller;
    private readonly Callers.ConfigurationCaller _configurationCaller;
    private readonly Callers.UserCaller _userCaller;

    public DefaultClient(
        Callers.AuthenticationCaller authenticationCaller,
        Callers.ConfigurationCaller configurationCaller,
        Callers.UserCaller userCaller)
    {
        _authenticationCaller = authenticationCaller;
        _configurationCaller = configurationCaller;
        _userCaller = userCaller;
    }

    public async Task<ApiResultResponse<bool>> VerifiedAsync(Guid roleId, string resource, string scope, string action)
    {
        var roleResponse = await _authenticationCaller.GetRoleDetailAsync(roleId);
        if (roleResponse.Success)
        {
            var response = roleResponse.Data!.Permissions
                .Any(permission => permission.Resource.Equals(scope, StringComparison.OrdinalIgnoreCase) &&
                    (permission.Scope.Equals(scope, StringComparison.OrdinalIgnoreCase) ||
                        permission.Scope.Equals("*", StringComparison.OrdinalIgnoreCase)) &&
                    (permission.Action.Equals(action, StringComparison.OrdinalIgnoreCase) ||
                        permission.Action.Equals("all", StringComparison.OrdinalIgnoreCase)));
            return ApiResultResponse<bool>.ResponseSuccess(response, Success);
        }
        return ApiResultResponse<bool>.ResponseLose(roleResponse.Message, default);
    }

    public async Task<ApiResultResponse<List<MenuInfoResponse>>> GetMenu(Guid roleId)
    {
        var roleResponse = await _authenticationCaller.GetRoleDetailAsync(roleId);
        if (!roleResponse.Success)
            return ApiResultResponse<List<MenuInfoResponse>>.ResponseLose(roleResponse.Message, new List<MenuInfoResponse>());

        var menuListResponse = _configurationCaller.GetAllAsync();
        if (!roleResponse.Success)
            return ApiResultResponse<List<MenuInfoResponse>>.ResponseLose(roleResponse.Message, new List<MenuInfoResponse>());

        List<MenuInfoResponse> menuList = new();
        foreach (var menu in menuList)
        {
            if (roleResponse.Data!.Permissions.Any(permission =>
                    permission.ObjectType == ObjectType.Menu &&
                    (permission.Scope.Equals("all") || (permission.Scope.Equals(menu.Id.ToString(), StringComparison.OrdinalIgnoreCase)) &&
                        permission.Action.Equals("view", StringComparison.OrdinalIgnoreCase))))
            {
                menuList.Add(menu);
            }
        }
        return ApiResultResponse<List<MenuInfoResponse>>.ResponseSuccess(menuList, Success);
    }
}
