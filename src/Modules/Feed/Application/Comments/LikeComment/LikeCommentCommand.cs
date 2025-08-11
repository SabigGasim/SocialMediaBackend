using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Authorization;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.LikeComment;

[HasPermission(Permissions.LikeComment)]
public sealed class LikeCommentCommand(Guid commentId) : CommandBase, IRequireAuthorization
{
    public CommentId CommentId { get; } = new(commentId);
}
