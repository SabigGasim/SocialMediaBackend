using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Authorization;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.EditComment;

[HasPermission(Permissions.UpdateComment)]
public sealed class EditCommentCommand(Guid commentId, string text) : CommandBase, IRequireAuthorization
{
    public CommentId CommentId { get; } = new(commentId);
    public string Text { get; } = text;
}
