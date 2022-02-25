namespace MASA.Framework.Admin.Service.User.Application.UserGroups.Queres
{
    public record UserGroupQuery(Guid UserId) : Query<List<UserGroupItemResponse>>
    {
        public override List<UserGroupItemResponse> Result { get; set; } = new();

    }
}
