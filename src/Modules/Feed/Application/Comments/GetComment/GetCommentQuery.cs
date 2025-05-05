using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Comments;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.GetComment;

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
