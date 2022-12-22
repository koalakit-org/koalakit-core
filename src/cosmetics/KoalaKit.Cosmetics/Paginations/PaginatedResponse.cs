
using KoalaKit.Cosmetics.Paginations;

namespace KoalaKit.Cosmetics
{
    [Serializable]
    public class PaginatedResponse<T> : KoalaEnvelope<IEnumerable<T>>
    {
        public PaginatedResponse(IEnumerable<T> items, int count, PaginationParameters pagination)
        {
            PageInfo = new PageInfo(count, pagination);
            Data = items;
        }
        public PageInfo PageInfo { get; set; }
    }
}
