namespace SocialMediaBackend.Modules.Users.Application.Abstractions;

public abstract record PagedResponse(int PageNumber, int PageSize, int TotalCount)
{
    public bool HasMorePages => TotalCount > PageNumber * PageSize;
}

public abstract record PagedResponse<T> : PagedResponse
{
    protected PagedResponse(
        int PageNumber, 
        int PageSize, 
        int TotalCount, 
        IEnumerable<T> items) : base(PageNumber, PageSize, TotalCount)
    {
        Items = items;
    }

    public IEnumerable<T> Items { get; }
}
