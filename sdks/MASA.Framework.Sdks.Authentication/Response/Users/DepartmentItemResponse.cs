namespace MASA.Framework.Sdks.Authentication.Response.Users
{
    public class DepartmentItemResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<DepartmentItemResponse> Children { get; set; } = new();
    }
}
