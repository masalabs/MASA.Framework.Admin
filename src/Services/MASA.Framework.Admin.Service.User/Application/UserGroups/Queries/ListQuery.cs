namespace Masa.Framework.Admin.Service.User.Application.UserGroups.Queries
{
    public record ListQuery(int PageIndex, int PageSize, string Name) : Query<PaginatedItemResponse<UserGroupItemResponse>>
    {
        public override PaginatedItemResponse<UserGroupItemResponse> Result { get; set; }
    }
}
