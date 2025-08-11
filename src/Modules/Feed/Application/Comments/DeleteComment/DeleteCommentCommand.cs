using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Authorization;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.DeleteComment;

[HasPermission(Permissions.DeleteComment)]
public sealed class DeleteCommentCommand(Guid commentId, Guid postId) : CommandBase, IRequireAuthorization
{
    public CommentId CommentId { get; } = new(commentId);
    public PostId PostId { get; } = new(postId);
}
