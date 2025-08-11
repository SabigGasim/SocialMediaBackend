using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Domain.Authorization;
using SocialMediaBackend.BuildingBlocks.Application.Auth;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.UpdatePost;

[HasPermission(Permissions.UpdatePost)]
public sealed class UpdatePostCommand(Guid postId, string text) : CommandBase, IRequireAuthorization
{
    public PostId PostId { get; } = new(postId);
    public string Text { get; } = text;
}
