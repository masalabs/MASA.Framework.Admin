namespace MASA.Framework.Admin.Contracts.Base.Queries;

public record QueryItems<TResult>(int PageIndex, int PageSize) : Query<TResult>
{
    public int PageIndex { get; set; } = PageIndex;

    public int PageSize { get; set; } = PageSize;

    public override TResult Result { get; set; } = default!;
}
