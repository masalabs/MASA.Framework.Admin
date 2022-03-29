namespace Masa.Framework.Admin.Service.User.Domain.Aggregates;

public class Department : AuditAggregateRoot<Guid, Guid>, ISoftDelete
{
    public string Name { get; private set; }

    public string Code { get; private set; }

    public string Describtion { get; private set; }

    public Guid ParentId { get; private set; } = Guid.Empty;

    private List<DepartmentUser> departmentUsers = new List<DepartmentUser>();

    public virtual IReadOnlyCollection<DepartmentUser> DepartmentUsers => departmentUsers;

    public Department(string name, string code, string describtion) : this(name, code, describtion, Guid.Empty)
    {

    }

    public Department(string name, string code, string describtion, Guid parentId)
    {
        Name = name;
        Code = code;
        Describtion = describtion;
        ParentId = parentId;
    }

    public void UpdateUsers(List<Guid> userIds)
    {
        departmentUsers.Clear();
        foreach (var userId in userIds)
        {
            departmentUsers.Add(new DepartmentUser(userId));
        }
    }
}

