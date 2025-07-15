using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Contracts;
using SocialMediaBackend.Modules.Feed.Domain.Authorization;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;
using SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Roles;

namespace SocialMediaBackend.Modules.Feed.Application.AppSubscriptions.ActivateAppSubscription;

internal sealed class ActivateAppSubscriptionCommandHandler(FeedDbContext context)
    : ICommandHandler<ActivateAppSubscriptionCommand>
{
    private readonly FeedDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(ActivateAppSubscriptionCommand command, CancellationToken ct)
    {
        var roleId = command.SubscriptionTier switch
        {
            AppSubscriptionTier.Basic => Roles.AppBasicPlan,
            AppSubscriptionTier.Plus => Roles.AppPlusPlan,
            _ => throw new NotImplementedException($"Tier {command.SubscriptionTier} is not supported")
        };

        var roleAlreadyGranted = await _context
            .Set<AuthorRole>()
            .AnyAsync(x =>
                x.AuthorId == command.AuthorId &&
                x.RoleId == roleId,
                CancellationToken.None);

        if (!roleAlreadyGranted)
        {
            _context.Set<AuthorRole>().Add(new AuthorRole(roleId, command.AuthorId));
        }

        return HandlerResponseStatus.NoContent;
    }
}
