using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.GetAuthor;

public class GetAuthorCommand(Guid authorId) : CommandBase<GetAuthorResponse>
{
    public AuthorId AuthorId { get; } = new(authorId);
}
