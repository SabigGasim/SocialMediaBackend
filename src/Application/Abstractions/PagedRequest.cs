using FastEndpoints;

namespace SocialMediaBackend.Application.Abstractions;

public abstract record PagedRequest
{
    [QueryParam, BindFrom("page")]
    public required int Page { get; init; } = 1;
    [QueryParam, BindFrom("pageSize")]
    public required int PageSize { get; init; } = 10;
}
