using FastEndpoints;

namespace SocialMediaBackend.BuildingBlocks.Application;

public abstract record PagedRequest
{
    [QueryParam, BindFrom("page")]
    public required int Page { get; init; } = 1;
    [QueryParam, BindFrom("pageSize")]
    public required int PageSize { get; init; } = 10;
}
