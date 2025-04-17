using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Infrastructure.Domain.Posts;

namespace SocialMediaBackend.Application.Posts.GetAllPosts;

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
