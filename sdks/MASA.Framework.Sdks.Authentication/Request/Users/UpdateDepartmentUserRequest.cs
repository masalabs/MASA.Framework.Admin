namespace Masa.Framework.Sdks.Authentication.Request.Users;

public class UpdateDepartmentUserRequest
{
    public Guid DepartmentId { get; set; }

    public List<Guid> UserIds { get; set; } = new();
}

