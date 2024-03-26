namespace BookARoom.Utilities;

public class PagedList<T> : List<T>
{
    public PageMetadata PageMetadata { get; set; }

    public PagedList(List<T> items, int itemsCount, int pageNumber, int pageSize)
    {
        PageMetadata = new PageMetadata
        {
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(itemsCount / (decimal)pageSize),
            PageSize = pageSize,
            TotalItems = itemsCount,
        };

        AddRange(items);
    }

    /// <summary>
    /// Returns a paginated list of source items with respect to the page number, size,
    /// and amount of source items
    /// </summary>
    /// <param name="source"></param>
    /// <param name="itemsCount"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static PagedList<T> ToPagedList(IEnumerable<T> source, int itemsCount,
        int pageNumber, int pageSize)
    {
        var items = source.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedList<T>(items, itemsCount, pageNumber, pageSize);
    }
}
