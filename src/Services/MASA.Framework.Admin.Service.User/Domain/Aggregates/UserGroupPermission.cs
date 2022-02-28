namespace Masa.Framework.Admin.Service.User.Domain.Aggregates;

public class UserGroupPermission : Entity<Guid>
{
    public Guid PermissionId { get; private set; }

    public UserGroup UserGroup { get; private set; }

    private UserGroupPermission()
    {
    }

    public UserGroupPermission(Guid permissionId) : this()
    {
        PermissionId = permissionId;
    }
}

