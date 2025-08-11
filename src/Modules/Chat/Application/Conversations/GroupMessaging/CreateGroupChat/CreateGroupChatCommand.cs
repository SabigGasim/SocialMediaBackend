using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupChat;

[HasPermission(Permissions.CreateGroupChat)]
public sealed class CreateGroupChatCommand(string groupName, IEnumerable<Guid> memberIds)
    : MultipleUsersCommandBase<CreateGroupChatMessage, CreateGroupChatResponse>, IRequireAuthorization
{
    public string GroupName { get; } = groupName;
    public IEnumerable<Guid> MemberIds { get; } = memberIds;
}
