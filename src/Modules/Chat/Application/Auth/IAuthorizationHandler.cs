using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Application.Auth;

public interface IAuthorizationHandler<TResource>
{
    bool Authorize(
        ChatterId? chatterId,
        TResource resource);
}
