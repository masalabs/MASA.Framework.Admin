namespace MASA.Framework.Admin.Contracts.Blogs;

public class PagingResult<T>
{
    public List<T> Data { get; set; }

    public int Page { get; set; }

    public int Size { get; set; }

    public int TotalCount { get; set; }
}