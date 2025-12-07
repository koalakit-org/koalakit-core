namespace Koalakit.Primitives.Paginations;

public sealed class PaginatedResponse<T> where T : PaginatedItem
{
    public PaginatedResponse(IEnumerable<T> items, int totalRows, PaginationParameters? pagination)
    {
        TotalRows = totalRows;
        PageNumber = pagination?.PageNumber ?? 0;
        PageSize = pagination?.PageSize ?? 0;
        if (PageSize > 0)
            TotalPages = (int)Math.Ceiling((double)totalRows / PageSize);

        int startNumber = (PageNumber - 1) * PageSize + 1;

        Items = [.. items.Select(item =>
        {
            item.Number = startNumber++;
            return item;
        })];
    }

    public PaginatedResponse(PaginationParameters pagination)
    {
        Items = Enumerable.Empty<T>().ToList();
        TotalRows = 0;
        PageNumber = pagination.PageNumber;
        PageSize = pagination.PageSize;
        TotalPages = 0;
    }

    public IList<T> Items { get; }
    public int TotalRows { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int PageSize { get; }

    public static PaginatedResponse<T> Empty(PaginationParameters pagination) => new(pagination);
    public static PaginatedResponse<T> Empty() => new(new() { PageNumber = 0, PageSize = 0 });
}