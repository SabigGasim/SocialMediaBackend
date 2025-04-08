namespace SocialMediaBackend.Application.Abstractions.Requests.Queries;

public class PagedQueryBase<TPagedResponse>(int page, int pageSize) : QueryBase<TPagedResponse>
    where TPagedResponse : PagedResponse
{
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
}
