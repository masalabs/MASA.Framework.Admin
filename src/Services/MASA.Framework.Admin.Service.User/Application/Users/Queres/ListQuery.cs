namespace MASA.Framework.Admin.Service.User.Application.Users.Queres;

public record ListQuery(int PageIndex, int PageSize, string Account) : Query<List<UserItemResponse>>
{
    public long Total { get; set; }

    public override List<UserItemResponse> Result { get; set; } = new List<UserItemResponse>();
}

