using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Application.Chatters.GetChatter;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupChat;

public record CreateGroupChatMessage(
    Guid Id,
    string Name,
    IEnumerable<GetChatterShortResponse> Members) : IRealtimeMessage;
