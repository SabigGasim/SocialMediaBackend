using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.PromoteMember;

internal sealed class PromoteMemberCommandHandler(
    ChatDbContext context,
    IAuthorizationHandler<GroupChat, GroupChatId> authorizationHandler)
    : IGroupCommandHandler<PromoteMemberCommand, MemberPromotedMessage>
{
    private readonly ChatDbContext _context = context;
    private readonly IAuthorizationHandler<GroupChat, GroupChatId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse<GroupResponse<MemberPromotedMessage>>> ExecuteAsync(PromoteMemberCommand command, CancellationToken ct)
    {
        var promoterId = new ChatterId(command.UserId);

        var authorizationResult = await _authorizationHandler.AuthorizeAsync(promoterId, command.GroupChatId, ct);

        if (!authorizationResult.IsSuccess)
        {
            return authorizationResult;
        }

        var group = await _context.GroupChats
            .Include(x => x.Members.Where(m =>
                   m.MemberId == command.MemberId
                || m.MemberId == promoterId))
            .Where(x => x.Id == command.GroupChatId)
            .FirstAsync(ct);

        var result = group.PromoteMember(promoterId, command.MemberId, command.Membership);
        if (!result.IsSuccess)
        {
            return result;
        }

        var response = new GroupResponse<MemberPromotedMessage>
        {
            Method = ChatHubMethods.ReceiveMemberPromoted,
            Message = new(group.Id.Value, command.MemberId.Value, command.Membership),
            ReceiverId =  group.Id.Value.ToString()
        };

        return (response, HandlerResponseStatus.NoContent);
    }
}
