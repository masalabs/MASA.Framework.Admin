using MASA.Framework.Admin.Contracts.Authentication.Request.Permissions;

namespace MASA.Framework.Admin.Service.Authentication.Services;

public class PermissionService : CustomServiceBase
{
    public PermissionService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.PermissionList, GetItemsAsync);
        App.MapGet(Routing.PermissionDetail, GetAsync);
        App.MapPost(Routing.OperatePermission, CreateAsync);
        App.MapPut(Routing.OperatePermission, EditAsync);
    }

    #region second stage .. Manage permissions individually

    /// <summary>
    /// Get a list of all permissions
    /// </summary>
    /// <param name="eventBus"></param>
    /// <param name="pageIndex">default: 1</param>
    /// <param name="pageSize">default: 20</param>
    /// <param name="objectType"></param>
    /// <param name="permissionType"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public ApiResultResponse<PaginatedItemResponse<PermissionItemResponse>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = Config.DEFAULT_PAGE_INDEX,
        [FromQuery] int pageSize = Config.DEFAULT_PAGE_SIZE,
        [FromQuery] int objectType = -1,
        [FromQuery] int permissionType = -1,
        [FromQuery] string name = "")
    {
        var response = new PaginatedItemResponse<PermissionItemResponse>(pageIndex, pageSize, 0, new List<PermissionItemResponse>());
        return Success(response);
    }

    public ApiResultResponse<PermissionDetailResponse> GetAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid id)
    {
        var response = new PermissionDetailResponse();
        return Success(response);
    }

    public ApiResultResponseBase CreateAsync(
        [FromServices] IEventBus eventBus,
        [FromServices] CreatePermissionRequest command)
    {
        return Success();
    }

    public ApiResultResponseBase EditAsync(
        [FromServices] IEventBus eventBus,
        [FromServices] CreatePermissionRequest command)
    {
        return Success();
    }

    #endregion
}
