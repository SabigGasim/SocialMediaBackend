using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Application.DirectMessaging.CreateDirectChat;

public class CreateDirectChatCommand(Guid otherChatterId) : CommandBase<CreateDirectChatResponse>, IUserRequest
{
    public Guid UserId { get; private set; }

    public bool IsAdmin { get; private set; }
    public ChatterId OtherChatterId { get; } = new(otherChatterId);

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
