using SocialMediaBackend.Application.Abstractions.Requests.Commands;

namespace SocialMediaBackend.Application.Comments.DeleteComment;

public class DeleteCommentCommand(Guid commentId, Guid postId) : CommandBase
{
    public Guid CommentId { get; } = commentId;
    public Guid PostId { get; } = postId;
}
