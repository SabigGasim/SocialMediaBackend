namespace SocialMediaBackend.Infrastructure.Common;

public readonly record struct PagedDto<T>(IEnumerable<T> Items, int Page, int PageSize, int TotalCount);
