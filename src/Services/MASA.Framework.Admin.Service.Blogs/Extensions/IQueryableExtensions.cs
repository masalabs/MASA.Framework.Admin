namespace System.Linq;

public static class IQueryableExtensions
{
    public static PagingResult<T> Paging<T>(this IQueryable<T> query, int page, int pageSize)
    {
        if (page < 1)
        {
            // todo Internationalization
            throw new ArgumentException("Page should be greater than 1", nameof(page));
        }

        if (pageSize < 1)
        {
            // todo Internationalization
            throw new ArgumentException("Size should be greater than 1", nameof(pageSize));
        }

        var result = new PagingResult<T>
        {
            TotalCount = query.Count(),
            Page = page,
            Size = pageSize,
            Data = query.Skip((page - 1) * pageSize).Take(pageSize).ToList()
        };
        return result;
    }

    public static async Task<PagingResult<T>> PagingAsync<T>(this IQueryable<T> query, int page, int pageSize)
    {
        if (page < 1)
        {
            // todo Internationalization
            throw new ArgumentException("Page should be greater than 1", nameof(page));
        }

        if (pageSize < 1)
        {
            // todo Internationalization
            throw new ArgumentException("Size should be greater than 1", nameof(pageSize));
        }

        var result = new PagingResult<T>
        {
            TotalCount = await query.CountAsync(),
            Page = page,
            Size = pageSize,
            Data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync()
        };
        return result;
    }
}