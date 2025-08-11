using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Domain.Authorization;
using SocialMediaBackend.BuildingBlocks.Application.Auth;


namespace SocialMediaBackend.Modules.Feed.Application.Comments.UnlikeComment;

[HasPermission(Permissions.UnlikeComment)]
public sealed class UnlikeCommentCommand(Guid commentId) : CommandBase, IRequireAuthorization
{
    public CommentId CommentId { get; } = new(commentId);
}
