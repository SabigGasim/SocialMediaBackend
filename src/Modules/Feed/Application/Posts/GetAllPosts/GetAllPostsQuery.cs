using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.GetAllPosts;

public sealed class GetAllPostsQuery(
    int page,
    int pageSize,
    Order order,
    string? text,
    DateOnly? since,
    DateOnly? until,
    string? idOrUsername) : QueryBase<GetAllPostsResponse>, IRequireOptionalAuthorizaiton
{
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
    public Order Order { get; } = order;
    public string? Text { get; } = text;
    public DateOnly? Since { get; } = since;
    public DateOnly? Until { get; } = until;
    public string? IdOrUsername { get; } = idOrUsername;
}
