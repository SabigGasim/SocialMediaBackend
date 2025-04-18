using SocialMediaBackend.Application.Abstractions.Requests.Queries;

namespace SocialMediaBackend.Application.Comments.GetComment;

public class GetCommentQuery(Guid commentId) : QueryBase<GetCommentResponse>
{
    public Guid CommentId { get; } = commentId;
}
