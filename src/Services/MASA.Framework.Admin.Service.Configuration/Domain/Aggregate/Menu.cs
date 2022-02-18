using MASA.Framework.Admin.Contracts.Base.Enum;

namespace MASA.Framework.Admin.Configuration.Domain.Aggregate;

public class Menu : AuditAggregateRoot<Guid, Guid>
{
    public string Code { get; private set; }

    public string Name { get;  set; } 

    public string? Describe { get; set; } 

    public string? Icon { get; set; } 

    public Guid? ParentId { get; set; }

    public string? ParentName { get; set; }

    public string Url { get; set; }

    public int Sort { get; set; }

    public State State { get; private set; }

    private Menu()
    {
        Name = String.Empty;
        Code = String.Empty;
        Url = String.Empty;
    }

    public Menu(Guid creator, string name,string code,string url, int sort, bool disabled)
    {
        Creator = creator;
        Modifier = creator;    
        Name = name;
        Code = code;
        Url = url;
        Sort = sort;
        State = disabled ? State.Disabled : State.Enable;
    }

    public void Update(Guid creator, string name, string url, int sort, bool disabled)
    {
        Modifier = creator;
        ModificationTime = DateTime.Now;
        Name = name;
        Url = url;
        Sort = sort;
        State = disabled ? State.Disabled : State.Enable;
    }
}
