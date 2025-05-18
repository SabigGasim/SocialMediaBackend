using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;

namespace SocialMediaBackend.Modules.Chat.Application.Auth;

internal class DirectChatAuthorizationHandler : IAuthorizationHandler<DirectChat>
{
    public bool Authorize(ChatterId? chatterId, DirectChat resource)
    {
        return chatterId == resource.FirstChatterId
            || chatterId == resource.SecondChatterId;
    }
}
