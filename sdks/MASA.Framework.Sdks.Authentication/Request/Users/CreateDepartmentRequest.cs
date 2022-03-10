namespace Masa.Framework.Sdks.Authentication.Request.Users;

public class CreateDepartmentRequest
{
    public string Name { get; set; }

    public string Code { get; set; }

    public string Describtion { get; set; }

    public Guid ParentId { get; set; }

    public string ParentName { get; set; }
}

