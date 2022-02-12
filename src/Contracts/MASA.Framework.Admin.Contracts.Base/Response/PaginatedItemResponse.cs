namespace MASA.Framework.Admin.Contracts.Base.Response;

public class PaginatedItemResponse<TEntity> where TEntity : class
{
    public int PageIndex { get; private set; }

    public int PageSize { get; private set; }

    public long Count { get; private set; }

    public long TotalPages { get; private set; }

    public IEnumerable<TEntity> Items { get; private set; }

    public PaginatedItemResponse(
        int pageIndex,
        int pageSize,
        long count,
        long totalPages,
        IEnumerable<TEntity> items)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        TotalPages = totalPages;
        Items = items;
    }

    public PaginatedItemResponse(
        QueryItems<PaginatedItemResponse<TEntity>> queryItems,
        long totalPages,
        long count,
        IEnumerable<TEntity> items)
        : this(queryItems.PageIndex, queryItems.PageSize, count, totalPages, items)
    {
    }
}
