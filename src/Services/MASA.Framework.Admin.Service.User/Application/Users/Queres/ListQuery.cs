namespace MASA.Framework.Admin.Service.User.Application.Users.Queres;

public record ListQuery(int PageIndex, int PageSize, string Account) : Query<PaginatedItemResponse<UserItemResponse>>
{
    public override PaginatedItemResponse<UserItemResponse> Result { get; set; } = default!;
}

