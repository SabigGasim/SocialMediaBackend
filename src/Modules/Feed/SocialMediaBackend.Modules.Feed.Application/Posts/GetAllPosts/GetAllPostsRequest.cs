using FastEndpoints;
using SocialMediaBackend.BuildingBlocks.Application;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.GetAllPosts;

public record GetAllPostsRequest : PagedRequest
{
    [QueryParam, BindFrom("idOrUsername")]
    public required string? IdOrUsername { get; init; } = null;
    [QueryParam, BindFrom("text")]
    public required string? Text { get; init; } = null;
    [QueryParam, BindFrom("since")]
    public required DateOnly? Since { get; init; } = null;
    [QueryParam, BindFrom("until")]
    public required DateOnly? Until { get; init; } = null;
    [QueryParam, BindFrom("order")]
    public required string? Order { get; init; } = null;
}
