namespace MASA.Framework.Sdks.Authentication.Request.Users;

public class CreateDepartmentRequest
{
    public string Name { get; set; }

    public string Code { get; set; }

    public string Describtion { get; set; }

    public int ParentId { get; set; }
}

