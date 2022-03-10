namespace Masa.Framework.Admin.Service.User.Application.Users.Queries;

public record ListQuery(int PageIndex, int PageSize, string Account) : Query<PaginatedItemResponse<UserItemResponse>>
{
    public override PaginatedItemResponse<UserItemResponse> Result { get; set; } = default!;
}

