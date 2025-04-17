using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Infrastructure.Domain.Posts;

namespace SocialMediaBackend.Application.Posts.GetAllPosts;

public class GetAllPostsQuery : QueryBase<GetAllPostsResponse>
{
    public GetAllPostsQuery(
        int page, 
        int pageSize, 
        Order order,
        string? text,
        DateOnly? since,
        DateOnly? until,
        string? idOrUsername)
    {
        Options = new GetAllPostsOptions
        {
            Page = page,
            PageSize = pageSize,
            Order = order,
            Text = text,
            Since = since,
            Until = until,
            IdOrUsername = idOrUsername
        };
    }

    public GetAllPostsOptions Options { get; init; }
}
