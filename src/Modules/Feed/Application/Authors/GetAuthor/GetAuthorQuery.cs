using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.GetAuthor;

public class GetAuthorQuery(Guid authorId) : QueryBase<GetAuthorResponse>
{
    public AuthorId AuthorId { get; } = new(authorId);
}
