using Autofac;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Users.DeleteUser;

internal sealed class DeleteUserCommandHandler(SubscriptionsDbContext context) : ICommandHandler<DeleteUserCommand>
{
    private readonly SubscriptionsDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(DeleteUserCommand command, CancellationToken ct)
    {
        await _context.Users
            .Where(x => x.Id == command.UserId)
            .ExecuteDeleteAsync(ct);

        return HandlerResponseStatus.NoContent;
    }
}
