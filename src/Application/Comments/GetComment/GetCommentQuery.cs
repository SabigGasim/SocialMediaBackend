using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Domain.Comments;

namespace SocialMediaBackend.Application.Comments.GetComment;

public class GetCommentQuery(Guid commentId) : QueryBase<GetCommentResponse>
{
    public CommentId CommentId { get; } = new(commentId);
}
