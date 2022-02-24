namespace MASA.Framework.Sdks.Authentication.Response.Users
{
    public class UserGroupItemResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Describtion { get; set; }

        public string Code { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime ModificationTime { get; set; }
    }
}
