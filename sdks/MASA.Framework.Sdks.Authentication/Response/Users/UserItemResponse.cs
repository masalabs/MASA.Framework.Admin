namespace Masa.Framework.Sdks.Authentication.Response.Users;

public class UserItemResponse
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string Account { get; set; } = default!;

    public bool? Gender { get; set; }

    public string? Cover { get; set; }

    public string? Email { get; set; }

    public int State { get; set; }

    public DateTimeOffset LastLoginTime { get; set; }

    public bool Select { get; set; }
}

