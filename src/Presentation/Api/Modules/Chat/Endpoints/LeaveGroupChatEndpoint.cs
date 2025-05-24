using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Api.Services;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.LeaveGroupChat;
using SocialMediaBackend.Modules.Chat.Application.Hubs;

namespace SocialMediaBackend.Api.Modules.Chat.Endpoints;

public class LeaveGroupChatEndpoint(
    IChatModule module,
    IRealtimeMessageSender<ChatHub> sender)
    : RealtimeEndpoint<LeaveGroupChatRequest, GroupChatLeftMessage, ChatHub>(module, sender)
{
    public override void Configure()
    {
        Delete(ApiEndpoints.GroupChat.LeaveGroupChat);
        Description(x => x.Accepts<LeaveGroupChatRequest>());
    }

    public override async Task HandleAsync(LeaveGroupChatRequest req, CancellationToken ct)
    {
        await HandleGroupCommandAsync(new LeaveGroupChatCommand(req.ChatId), ct);
    }
}
