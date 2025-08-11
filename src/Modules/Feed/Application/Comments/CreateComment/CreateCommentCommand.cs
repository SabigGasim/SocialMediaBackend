using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Authorization;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.CreateComment;

[HasPermission(Permissions.CreateComment)]
public sealed class CreateCommentCommand(Guid postId, string text) : CommandBase<CreateCommentResponse>, IRequireAuthorization
{
    public PostId PostId { get; } = new(postId);
    public string Text { get; } = text;
}
