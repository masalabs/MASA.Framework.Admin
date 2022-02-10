namespace MASA.Framework.Admin.Contracts.User.Response;

public class UserDetailResponse : UserItemResponse
{
    public DateTimeOffset CreationTime { get; set; }

    public DateTimeOffset LastUpdateTime { get; set; }
}
