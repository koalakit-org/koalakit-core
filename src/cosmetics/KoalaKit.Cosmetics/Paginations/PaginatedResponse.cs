
using KoalaKit.Cosmetics.Paginations;

namespace KoalaKit.Cosmetics
{
    [Serializable]
    public readonly struct PaginatedResponse<T>
    {
        public PaginatedResponse(int count, PaginationParameters pagination, ICollection<T>? items)
        {
            PageInfo = new PageInfo(count, pagination);
            Items = items ?? new List<T>();
        }


        public PaginatedResponse(int count, PaginationParameters pagination, params T[] items)
        {
            PageInfo = new PageInfo(count, pagination);
            Items = items;
        }

        public ICollection<T> Items { get; }
        public PageInfo PageInfo { get; }
    }
}
