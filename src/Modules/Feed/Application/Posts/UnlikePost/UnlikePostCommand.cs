using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Domain.Authorization;
using SocialMediaBackend.BuildingBlocks.Application.Auth;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.UnlikePost;

[HasPermission(Permissions.UnlikePost)]
public sealed class UnlikePostCommand(Guid postId) : CommandBase, IRequireAuthorization
{
    public PostId PostId { get; } = new(postId);
}