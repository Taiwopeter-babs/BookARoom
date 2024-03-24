namespace BookARoom.Utilities;

public class RequestParameters
{
    private int _pageSize = 10;
    const int MaxPageSize = 20;
    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set { _pageSize = (value <= MaxPageSize) ? value : _pageSize; }
    }
}
