using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Users.Infrastructure.Domain.Posts;

namespace SocialMediaBackend.Modules.Users.Application.Posts.GetAllPosts;

public class GetAllPostsQuery : QueryBase<GetAllPostsResponse>, IOptionalUserRequest<GetAllPostsQuery>
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

    public GetAllPostsQuery AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public GetAllPostsQuery WithUserId(Guid userId)
    {
        UserId = userId;
        Options.RequestingUserId = userId;

        return this;
    }
}
