namespace MASA.Framework.Admin.Service.User.Application.UserGroups.Queres
{
    public record ListQuery : Query<UserGroupItemResponse>
    {
        public override UserGroupItemResponse Result { get; set; }
    }
}
