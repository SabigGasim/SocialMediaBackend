using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Api.Services;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.KickGroupMember;
using SocialMediaBackend.Modules.Chat.Application.Hubs;

namespace SocialMediaBackend.Api.Modules.Chat.Endpoints;

public class KickGroupMemberEndpoint(
    IChatModule module,
    IRealtimeMessageSender<ChatHub> sender)
    : RealtimeEndpoint<KickGroupMemberRequest, KickGroupMemberMessage, ChatHub>(module, sender)
{
    public override void Configure()
    {
        Post(ApiEndpoints.GroupChat.KickGroupMember);
        Description(x => x.Accepts<KickGroupMemberRequest>());
    }

    public override async Task HandleAsync(KickGroupMemberRequest req, CancellationToken ct)
    {
        await HandleGroupCommandAsync(new KickGroupMemberCommand(req.MemberId, req.ChatId), ct);
    }
}
