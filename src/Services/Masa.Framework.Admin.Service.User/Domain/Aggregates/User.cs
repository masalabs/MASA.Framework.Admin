namespace Masa.Framework.Admin.Service.User.Domain.Aggregates;

public class User : FullAggregateRoot<Guid, Guid>
{
    public string Account { get; private set; } = default!;

    public string? Name { get; set; }

    public string Password { get; private set; } = default!;

    public string Salt { get; private set; } = default!;

    public bool Gender { get; set; }

    public string? Cover { get; set; }

    public string? Email { get; set; }

    public bool IsAdmin { get; private set; }

    public bool Enable { get; private set; } = true;

    public DateTimeOffset LastLoginTime { get; private set; }

    public DateTimeOffset LastUpdateTime { get; private set; }

    private List<UserRole> userRoles = new();

    public virtual IReadOnlyCollection<UserRole> UserRoles => userRoles;

    private List<UserGroupItem> userGroups = new();

    public virtual IReadOnlyCollection<UserGroupItem> UserGroups => userGroups;

    private List<DepartmentUser> departmentUsers = new List<DepartmentUser>();

    public virtual IReadOnlyCollection<DepartmentUser> DepartmentUsers => departmentUsers;

    public User(Guid id)
    {
        Id = id;
        Enable = true;
        LastLoginTime = DateTimeOffset.Now;
        LastUpdateTime = DateTimeOffset.Now;
    }

    public User(Guid? creator, string account, string password)
    {
        Account = account;
        Salt = RandomUtils.GenerateSpecifiedString(6);
        Password = SHA1Utils.Encrypt($"{password}{Salt}");
        Creator = creator ?? Id;
        Modifier = Creator;
    }

    public User(Guid? creator, string account, string password, bool isAdmin)
    {
        Account = account;
        Salt = RandomUtils.GenerateSpecifiedString(6);
        Password = SHA1Utils.Encrypt($"{password}{Salt}");
        Creator = creator ?? Id;
        Modifier = Creator;
        IsAdmin = isAdmin;
    }

    public void AddRole(Guid roleId)
    {
        if (!userRoles.Any(r => r.RoleId == roleId))
        {
            userRoles.Add(new UserRole(roleId));
        }
    }

    public void RemoveRole(Guid roleId)
    {
        if (userRoles.Any(r => r.RoleId == roleId))
        {
            userRoles.RemoveAll(r => r.RoleId == roleId);
        }
    }

    public void UpdateLastLoginTime()
    {
        LastLoginTime = DateTime.Now;
    }
}
