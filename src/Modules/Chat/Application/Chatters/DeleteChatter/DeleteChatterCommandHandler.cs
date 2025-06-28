using Autofac;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.DeleteChatter;

internal sealed class DeleteChatterCommandHandler(ChatDbContext context) : ICommandHandler<DeleteChatterCommand>
{
    private readonly ChatDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(DeleteChatterCommand command, CancellationToken ct)
    {
        await _context.Chatters
            .Where(x => x.Id == command.ChatterId)
            .ExecuteDeleteAsync(ct);

        return HandlerResponseStatus.NoContent;
    }
}
