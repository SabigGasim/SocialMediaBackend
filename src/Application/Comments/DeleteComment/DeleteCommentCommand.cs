using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Posts;

namespace SocialMediaBackend.Application.Comments.DeleteComment;

public class DeleteCommentCommand(Guid commentId, Guid postId) : CommandBase
{
    public CommentId CommentId { get; } = new(commentId);
    public PostId PostId { get; } = new(postId);
}
