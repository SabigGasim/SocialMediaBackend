using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.CreateAuthor;

public class CreateAuthorCommandHandler(FeedDbContext context) : ICommandHandler<CreateAuthorCommand>
{
    private readonly FeedDbContext _context = context;

    public Task<HandlerResponse> ExecuteAsync(CreateAuthorCommand command, CancellationToken ct)
    {
        var author = Author.Create(
            command.AuthorId,
            command.Username,
            command.Nickname,
            command.ProfilePicture,
            command.ProfileIsPublic,
            command.FollowersCount,
            command.FollowingCount);

        _context.Add(author);

        return Task.FromResult((HandlerResponse)HandlerResponseStatus.NoContent);
    }
}
