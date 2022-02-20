namespace MASA.Framework.Admin.Service.Authentication.Application.Objects.Queries;

public record ContainsObjectQuery(Guid ObjectId,string ObjectCode) : Query<bool>
{
    public override bool Result { get; set; }
}
