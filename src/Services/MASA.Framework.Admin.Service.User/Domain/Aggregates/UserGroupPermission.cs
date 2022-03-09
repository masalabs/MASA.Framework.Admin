namespace Masa.Framework.Admin.Service.User.Domain.Aggregates;

public class UserGroupPermission : Entity<Guid>
{
    public Guid PermissionId { get; private set; }

    public virtual UserGroup UserGroup { get; private set; }

    public UserGroupPermission(Guid permissionId)
    {
        PermissionId = permissionId;
    }
}

