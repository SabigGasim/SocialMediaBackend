using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Domain.Authorization;
using SocialMediaBackend.BuildingBlocks.Application.Auth;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.LikePost;

[HasPermission(Permissions.LikePost)]
public sealed class LikePostCommand(Guid postId) : CommandBase, IRequireAuthorization
{
    public PostId PostId { get; } = new(postId);
}
