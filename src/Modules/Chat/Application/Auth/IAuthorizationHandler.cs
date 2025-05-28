using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Application.Auth;

public interface IAuthorizationHandler<TResource>
{
    bool Authorize(
        ChatterId? chatterId,
        TResource resource);
}

public interface IAuthorizationHandler<TResource, TResourceId>
{
    Task<Result> AuthorizeAsync(
        ChatterId chatterId,
        TResourceId resourceId,
        CancellationToken ct = default);
}
