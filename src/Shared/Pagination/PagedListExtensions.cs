using Microsoft.EntityFrameworkCore;

namespace CompanyRatingApi.Shared.Pagination;

public static class PagedListExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> query,
        int page,
        int size,
        CancellationToken cancellationToken
    )
    {
        var total = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        return new PagedList<T>(items, page, size, total);
    }
}