namespace MASA.Framework.Sdks.Authentication.Response.Users;

public class UserDetailResponse : UserItemResponse
{
    public DateTimeOffset CreationTime { get; set; }

    public DateTimeOffset LastUpdateTime { get; set; }
}

