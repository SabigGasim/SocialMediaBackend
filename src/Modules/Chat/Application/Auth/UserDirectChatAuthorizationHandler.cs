using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;

namespace SocialMediaBackend.Modules.Chat.Application.Auth;

internal class UserDirectChatAuthorizationHandler : IAuthorizationHandler<UserDirectChat>
{
    public bool Authorize(ChatterId? chatterId, UserDirectChat resource)
    {
        return resource.ChatterId == chatterId;
    }
}
