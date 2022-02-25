namespace MASA.Framework.Admin.Service.User.Application.UserGroups.Queres
{
    public record SelectQuery(Guid UserId) : Query<List<UserGroupItemResponse>>
    {
        public override List<UserGroupItemResponse> Result { get; set; } = null!;
    }
}
