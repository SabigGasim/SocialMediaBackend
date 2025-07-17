using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Domain.Authorization;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;
using SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Roles;

namespace SocialMediaBackend.Modules.Feed.Application.AppSubscriptions.AppSubscriptionCanceled;

internal sealed class CancelAppSubscriptionCommandHandler(
    FeedDbContext context,
    IPermissionManager permissionManager)
    : ICommandHandler<CancelAppSubscriptionCommand>
{
    private readonly FeedDbContext _context = context;
    private readonly IPermissionManager _permissionManager = permissionManager;

    public async Task<HandlerResponse> ExecuteAsync(CancelAppSubscriptionCommand command, CancellationToken ct)
    {
        if (await _permissionManager.UserIsInRole(command.AuthorId.Value, (int)Roles.AdminAuthor))
        {
            return HandlerResponseStatus.NoContent;
        }

        var role = await _context.Set<AuthorRole>()
            .Where(x =>
                x.AuthorId == command.AuthorId &&
                (
                    x.RoleId == Roles.AppBasicPlan ||
                    x.RoleId == Roles.AppPlusPlan
                ))
            .FirstAsync(CancellationToken.None);

        _context.Set<AuthorRole>().Remove(role);

        return HandlerResponseStatus.NoContent;
    }
}
