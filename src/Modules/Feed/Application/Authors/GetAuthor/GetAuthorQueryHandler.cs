using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.GetAuthor;

internal sealed class GetAuthorQueryHandler(IAuthorRepository authorRepository) 
    : IQueryHandler<GetAuthorQuery, GetAuthorResponse>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<HandlerResponse<GetAuthorResponse>> ExecuteAsync(GetAuthorQuery command, CancellationToken ct)
    {
        var author = await _authorRepository.GetByIdAsync(command.AuthorId, ct);
        if (author is null)
        {
            return ("Author with the given Id was not found", HandlerResponseStatus.NotFound, command.AuthorId.Value);
        }

        return new GetAuthorResponse(
            author.Id, 
            author.Username, 
            author.Nickname, 
            author.FollowersCount, 
            author.FollowingCount, 
            author.ProfilePictureUrl);
    }
}
