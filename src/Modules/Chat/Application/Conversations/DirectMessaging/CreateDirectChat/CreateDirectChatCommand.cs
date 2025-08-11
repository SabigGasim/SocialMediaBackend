using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectChat;

[HasPermission(Permissions.CreateDirectChat)]
public sealed class CreateDirectChatCommand(Guid otherChatterId)
    : CommandBase<CreateDirectChatResponse>, IRequireAuthorization
{
    public ChatterId OtherChatterId { get; } = new(otherChatterId);
}
