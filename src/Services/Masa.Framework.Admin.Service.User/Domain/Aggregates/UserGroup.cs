namespace Masa.Framework.Admin.Service.User.Domain.Aggregates;

public class UserGroup : FullAggregateRoot<Guid, Guid>
{
    public string Name { get; private set; }

    public string Code { get; private set; }

    public string Describtion { get; private set; }

    private List<UserGroupItem> userGroupItems = new();

    public virtual IReadOnlyCollection<UserGroupItem> UserGroupItems => userGroupItems;

    private List<UserGroupPermission> userGroupPermissions = new();

    public virtual IReadOnlyCollection<UserGroupPermission> UserGroupPermissions => userGroupPermissions;

    public UserGroup(string name, string code, string describtion)
    {
        Name = name;
        Code = code;
        Describtion = describtion;
    }

    public void AddUser(Guid userId)
    {
        if (!userGroupItems.Any(r => r.UserId == userId))
        {
            userGroupItems.Add(new UserGroupItem(userId));
        }
    }

    public void RemoveUser(Guid userId)
    {
        if (userGroupItems.Any(r => r.UserId == userId))
        {
            userGroupItems.RemoveAll(r => r.UserId == userId);
        }
    }

    public void AddPermission(Guid permissionId)
    {
        if (!userGroupPermissions.Any(r => r.PermissionId == permissionId))
        {
            userGroupPermissions.Add(new UserGroupPermission(permissionId));
        }
    }

    public void RemovePermission(Guid permissionId)
    {
        if (userGroupPermissions.Any(r => r.PermissionId == permissionId))
        {
            userGroupPermissions.RemoveAll(r => r.PermissionId == permissionId);
        }
    }
}
