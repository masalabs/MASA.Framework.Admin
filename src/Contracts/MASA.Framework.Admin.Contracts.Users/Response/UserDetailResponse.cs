namespace MASA.Framework.Admin.Contracts.Users.Response;

public class UserDetailResponse:UserItemResponse
{
    public DateTimeOffset CreationTime { get; set; }

    public DateTimeOffset LastUpdateTime { get; set; }
}
