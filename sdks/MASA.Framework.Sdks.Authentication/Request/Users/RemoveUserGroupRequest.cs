namespace MASA.Framework.Sdks.Authentication.Request.Users
{
    public class RemoveUserGroupRequest
    {
        public Guid UserId { get; set; }

        public Guid UserGroupId { get; set; }
    }
}
