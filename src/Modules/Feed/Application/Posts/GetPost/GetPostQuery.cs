using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.GetPost;

public class GetPostQuery(Guid postId) : QueryBase<GetPostResponse>, IRequireOptionalAuthorizaiton
{
    public PostId PostId { get; } = new(postId);
}
