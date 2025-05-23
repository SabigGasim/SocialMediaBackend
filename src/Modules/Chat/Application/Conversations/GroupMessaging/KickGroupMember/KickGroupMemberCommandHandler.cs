using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.KickGroupMember;

public class KickGroupMemberCommandHandler(ChatDbContext context)
    : IGroupCommandHandler<KickGroupMemberCommand, KickGroupMemberMessage>
{
    private readonly ChatDbContext _context = context;

    public async Task<HandlerResponse<GroupResponse<KickGroupMemberMessage>>> ExecuteAsync(KickGroupMemberCommand command, CancellationToken ct)
    {
        var kickerId = new ChatterId(command.UserId);

        var result = await _context.GroupChats
            .Include(x => x.Members.Where(x =>
                   x.MemberId == kickerId
                || x.MemberId == command.ChatterId))
            .Where(x => x.Id == command.GroupChatId)
            .Select(x => new
            {
                Kicker = x.Members.FirstOrDefault(x => x.MemberId == kickerId),
                ToKick = x.Members.FirstOrDefault(x => x.MemberId == command.ChatterId),
                GroupChat = x
            })
            .FirstOrDefaultAsync(ct);

        var error = ValidateKickRequest(result?.GroupChat, result?.Kicker, result?.ToKick, command.ChatterId);
        if (error is not null)
        {
            return error;
        }

        var groupChat = result!.GroupChat!;
        var memberToKick = result!.ToKick!;
        var kickerMember = result!.Kicker!;

        groupChat.RemoveMember(kickerMember, memberToKick);

        var response = new GroupResponse<KickGroupMemberMessage>
        {
            Method = ChatHubMethods.KickGroupMember,
            ReceiverId = groupChat.Id.Value.ToString(),
            Message = new(command.ChatterId.Value, command.GroupChatId.Value)
        };

        return (response, HandlerResponseStatus.NoContent);
    }

    private static HandlerResponse<GroupResponse<KickGroupMemberMessage>>? ValidateKickRequest(
        GroupChat? groupChat,
        GroupChatMember? kickerMember,
        GroupChatMember? memberToKick,
        ChatterId requestedToKickId)
    {
        return (groupChat, kickerMember, memberToKick) switch
        {
            { groupChat: null } => ("Group chat with the given Id was not found", HandlerResponseStatus.NotFound),
            { kickerMember: null } => ("You're unauthorized to view this group chat", HandlerResponseStatus.Unauthorized),
            { memberToKick: null } => ("Member with the given Id was not found", HandlerResponseStatus.NotFound, requestedToKickId.Value),
            //{ kickerMember: var kicker, memberToKick: var toKick }
            //  when kicker.MemberId == toKick.MemberId
            //    => ("You can't kick yourself", HandlerResponseStatus.Conflict),
            //{ kickerMember: var kicker, memberToKick: var toKick }
            //  when kicker.Membership <= toKick.Membership
            //    => ("You can't kick a member that has the same membership as yours or higher", HandlerResponseStatus.Unauthorized),
            _ => null
        };
    }
}
