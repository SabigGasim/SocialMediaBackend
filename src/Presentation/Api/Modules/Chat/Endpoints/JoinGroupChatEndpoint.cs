using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Api.Services;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.JoinGroupChat;
using SocialMediaBackend.Modules.Chat.Application.Hubs;

namespace SocialMediaBackend.Api.Modules.Chat.Endpoints;

public class JoinGroupChatEndpoint(
    IChatModule module,
    IRealtimeMessageSender<ChatHub> sender)
    : RealtimeEndpoint<JoinGroupChatRequest, GroupChatJoinedMessage, ChatHub>(module, sender)
{
    public override void Configure()
    {
        Post(ApiEndpoints.GroupChat.JoinGroupChat);
        Description(x => x.Accepts<JoinGroupChatRequest>());
    }

    public override async Task HandleAsync(JoinGroupChatRequest req, CancellationToken ct)
    {
        await HandleGroupCommandAsync(new JoinGroupChatCommand(req.ChatId), ct);
    }
}