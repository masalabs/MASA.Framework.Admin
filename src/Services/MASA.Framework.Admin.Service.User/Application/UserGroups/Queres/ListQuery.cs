namespace MASA.Framework.Admin.Service.User.Application.UserGroups.Queres
{
    public record ListQuery(int PageIndex, int PageSize, string Name) : Query<PaginatedItemResponse<UserGroupItemResponse>>
    {
        public override PaginatedItemResponse<UserGroupItemResponse> Result { get; set; }
    }
}
