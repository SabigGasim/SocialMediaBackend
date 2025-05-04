using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Domain.Feed.Comments;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Comments.GetComment;

public class GetCommentQuery(Guid commentId) 
    : QueryBase<GetCommentResponse>, IOptionalUserRequest<GetCommentQuery>
{
    public CommentId CommentId { get; } = new(commentId);

    public Guid? UserId { get; private set; } = default!;

    public bool IsAdmin { get; private set; }

    public GetCommentQuery AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public GetCommentQuery WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
