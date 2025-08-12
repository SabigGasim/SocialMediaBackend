using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Comments;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.GetComment;

public sealed class GetCommentQuery(Guid commentId) : QueryBase<GetCommentResponse>, IRequireOptionalAuthorizaiton
{
    public CommentId CommentId { get; } = new(commentId);
}
