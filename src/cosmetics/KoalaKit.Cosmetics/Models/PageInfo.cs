namespace KoalaKit.Cosmetics
{
    [Serializable]
    public struct PageInfo
    {
        public PageInfo(int totalCount, PaginationParameters pagination)
        {
            TotalCount = totalCount;
            PageNumber = pagination.PageNumber;
            PageSize = pagination.PageSize;
            TotalPages = (int)Math.Ceiling((double)totalCount/pagination.PageSize);
        }

        public int TotalCount { get; set; }

        public int PageNumber { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }
    }
}
