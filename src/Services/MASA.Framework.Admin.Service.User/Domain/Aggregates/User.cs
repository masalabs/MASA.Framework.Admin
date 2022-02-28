using Masa.Framework.Admin.Infrastructure.Utils;
using Masa.Utils.Security.Cryptography;

namespace Masa.Framework.Admin.Service.User.Domain.Aggregates;

public class User : AuditAggregateRoot<Guid, Guid>
{
    public string Account { get; private set; } = default!;

    public string? Name { get; set; }

    public string Password { get; private set; } = default!;

    public string Salt { get; private set; } = default!;

    public bool Gender { get; set; }

    public string? Cover { get; set; }

    public string? Email { get; set; }

    public bool Enable { get; private set; }

    public DateTimeOffset LastLoginTime { get; private set; }

    public DateTimeOffset LastUpdateTime { get; private set; }

    private List<UserRole> userRoles;

    public IReadOnlyCollection<UserRole> UserRoles => userRoles;

    private List<UserGroupItem> userGroups;

    public IReadOnlyCollection<UserGroupItem> UserGroups => userGroups;

    private List<DepartmentUser> departmentUsers = new List<DepartmentUser>();

    public IReadOnlyCollection<DepartmentUser> DepartmentUsers => departmentUsers;

    private User()
    {
        Id = Guid.NewGuid();
        Enable = true;
        LastLoginTime = DateTimeOffset.Now;
        LastUpdateTime = DateTimeOffset.Now;
        userRoles = new();
        userGroups = new();
    }

    public User(Guid id)
    {
        Id = id;
        Enable = true;
        LastLoginTime = DateTimeOffset.Now;
        LastUpdateTime = DateTimeOffset.Now;
        userRoles = new();
    }

    public User(Guid? creator, string account, string password) : this()
    {
        Account = account;
        Salt = RandomUtils.GenerateSpecifiedString(6);
        Password = SHA1Utils.Encrypt($"{password}{Salt}");
        Creator = creator ?? Id;
        Modifier = Creator;
        userRoles = new();
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
}
