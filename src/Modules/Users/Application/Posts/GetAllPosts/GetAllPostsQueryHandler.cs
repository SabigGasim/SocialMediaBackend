using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Modules.Users.Application.Common;
using SocialMediaBackend.Modules.Users.Application.Mappings;
using SocialMediaBackend.Modules.Users.Infrastructure.Domain.Posts;

namespace SocialMediaBackend.Modules.Users.Application.Posts.GetAllPosts;

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
