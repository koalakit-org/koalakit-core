namespace KoalaKit.Cosmetics
{
    [Serializable]
    public class PaginatedResponse<T> : KoalaElkomResult<IEnumerable<T>>
    {
        public PaginatedResponse(IEnumerable<T> items, int count, PaginationParameters pagination)
        {
            PageInfo = new PageInfo(count, pagination);
            Data = items;
        }

        public IEnumerable<T> Data { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
