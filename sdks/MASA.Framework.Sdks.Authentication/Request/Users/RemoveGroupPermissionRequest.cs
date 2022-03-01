namespace Masa.Framework.Sdks.Authentication.Request.Users
{
    public class RemoveGroupPermissionRequest
    {
        public Guid PermissionId { get; set; }

        public Guid UserGroupId { get; set; }
    }
}
