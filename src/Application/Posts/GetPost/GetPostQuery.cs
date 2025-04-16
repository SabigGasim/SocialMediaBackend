using SocialMediaBackend.Application.Abstractions.Requests.Queries;

namespace SocialMediaBackend.Application.Posts.GetPost;

public class GetPostQuery(Guid postId) : QueryBase<GetPostResponse>
{
    public Guid PostId { get; } = postId;
}
