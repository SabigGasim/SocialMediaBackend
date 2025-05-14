using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.GetAllPosts;

public class GetAllPostsQuery : QueryBase<GetAllPostsResponse>, IOptionalUserRequest
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

    public Guid? UserId { get; private set; }

    public bool IsAdmin { get => Options.IsAdmin; private set => Options.IsAdmin = value; }

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
        Options.RequestingUserId = userId;
    }
}
