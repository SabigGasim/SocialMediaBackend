using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.KickGroupMember;

public class KickGroupMemberCommandHandler(
    ChatDbContext context,
    IAuthorizationHandler<GroupChat, GroupChatId> authorizationHandler)
    : IGroupCommandHandler<KickGroupMemberCommand, KickGroupMemberMessage>
{
    private readonly ChatDbContext _context = context;
    private readonly IAuthorizationHandler<GroupChat, GroupChatId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse<GroupResponse<KickGroupMemberMessage>>> ExecuteAsync(KickGroupMemberCommand command, CancellationToken ct)
    {
        var kickerId = new ChatterId(command.UserId);

        var authorizationResult = await _authorizationHandler.AuthorizeAsync(kickerId, command.GroupChatId, ct);

        if (!authorizationResult.IsSuccess)
        {
            return authorizationResult;
        }

        var groupChat = await _context.GroupChats
            .Include(x => x.Members.Where(x =>
                   x.MemberId == kickerId
                || x.MemberId == command.ChatterId))
            .Where(x => x.Id == command.GroupChatId)
            .FirstAsync(ct);

        var result = groupChat.KickMember(kickerId, command.ChatterId);
        if (!result.IsSuccess)
        {
            return result;
        }

        var response = new GroupResponse<KickGroupMemberMessage>
        {
            Method = ChatHubMethods.KickGroupMember,
            ReceiverId = groupChat.Id.Value.ToString(),
            Message = new(command.ChatterId.Value, command.GroupChatId.Value)
        };

        return (response, HandlerResponseStatus.NoContent);
    }
}
