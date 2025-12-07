namespace Koalakit.Primitives.Paginations;

public record PaginationParameters
{
    public PaginationParameters()
    {
        PageSize = 10;
        PageNumber = 1;
    }

    public PaginationParameters(int pageSize, int pageNumber)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
    }

    public int PageSize { get; init; }
    public int PageNumber { get; init; }
}