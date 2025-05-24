using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Api.Services;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.PromoteMember;
using SocialMediaBackend.Modules.Chat.Application.Hubs;

namespace SocialMediaBackend.Api.Modules.Chat.Endpoints;

[HttpPost(ApiEndpoints.GroupChat.PromoteMember)]
public class PromoteMemberEndpoint(
    IChatModule module,
    IRealtimeMessageSender<ChatHub> sender)
    : RealtimeEndpoint<PromoteMemberRequest, MemberPromotedMessage, ChatHub>(module, sender)
{
    public override async Task HandleAsync(PromoteMemberRequest req, CancellationToken ct)
    {
        await HandleGroupCommandAsync(new PromoteMemberCommand(req.ChatId, req.MemberId, req.Membership), ct);
    }
}
