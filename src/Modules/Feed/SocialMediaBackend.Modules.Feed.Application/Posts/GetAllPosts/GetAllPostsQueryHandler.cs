using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Feed.Application.Mappings;
using SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.GetAllPosts;

public class GetAllPostsQueryHandler(IPostRepository repository) : IQueryHandler<GetAllPostsQuery, GetAllPostsResponse>
{
    private readonly IPostRepository _repository = repository;

    public async Task<HandlerResponse<GetAllPostsResponse>> ExecuteAsync(GetAllPostsQuery query, CancellationToken ct)
    {
        var (Items, Page, PageSize, TotalCount) = await _repository.GetAllAsync(query.Options, ct);
        var response = Items.MapToResponse(Page, PageSize, TotalCount);

        return (response, HandlerResponseStatus.OK);
    }
}
