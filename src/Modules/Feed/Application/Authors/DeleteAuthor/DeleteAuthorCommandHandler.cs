using Autofac;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.DeleteAuthor;

internal sealed class DeleteAuthorCommandHandler(FeedDbContext context) : ICommandHandler<DeleteAuthorCommand>
{
    private readonly FeedDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(DeleteAuthorCommand command, CancellationToken ct)
    {
        await _context.Authors
            .Where(x => x.Id == command.AuthorId)
            .ExecuteDeleteAsync(ct);

        return HandlerResponseStatus.NoContent;
    }
}
