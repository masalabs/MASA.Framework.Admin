namespace MASA.Framework.Admin.Service.User.Application.Users.Queres;

public record ListQuery : Query<List<UserItemResponse>>
{
    public override List<UserItemResponse> Result { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}

