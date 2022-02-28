namespace Masa.Framework.Admin.Service.User.Application.UserGroups.Queries
{
    public record UserGroupQuery(Guid UserId) : Query<List<UserGroupItemResponse>>
    {
        public override List<UserGroupItemResponse> Result { get; set; } = new();

    }
}
