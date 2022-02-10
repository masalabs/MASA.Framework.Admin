namespace MASA.Framework.Admin.Contracts.User.Response;

public class UserItemResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Account { get; set; }

    public bool? Gender { get; set; }

    public string Cover { get; set; }

    public string Email { get; set; }

    public int State { get; set; }

    public DateTimeOffset LastLoginTime { get; set; }
}
