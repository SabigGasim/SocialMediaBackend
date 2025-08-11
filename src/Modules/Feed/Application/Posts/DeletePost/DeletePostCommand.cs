using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Domain.Authorization;
using SocialMediaBackend.BuildingBlocks.Application.Auth;


namespace SocialMediaBackend.Modules.Feed.Application.Posts.DeletePost;

[HasPermission(Permissions.DeletePost)]
public sealed class DeletePostCommand(Guid postId) : CommandBase, IRequireAuthorization
{
    public PostId PostId { get; } = new(postId);
}
