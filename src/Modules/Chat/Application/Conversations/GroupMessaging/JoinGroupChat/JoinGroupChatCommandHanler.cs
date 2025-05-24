using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.JoinGroupChat;

public class JoinGroupChatCommandHanler(ChatDbContext context)
    : IGroupCommandHandler<JoinGroupChatCommand, GroupChatJoinedMessage>
{
    private readonly ChatDbContext _context = context;

    public async Task<HandlerResponse<GroupResponse<GroupChatJoinedMessage>>> ExecuteAsync(JoinGroupChatCommand command, CancellationToken ct)
    {
        var chatterId = new ChatterId(command.UserId);

        var group = await _context.GroupChats
            .Where(x => x.Id == command.GroupChatId)
            .Include(x => x.Members.Where(m => m.MemberId == chatterId))
            .FirstOrDefaultAsync(ct);

        if (group is null)
        {
            return ("Group with the given Id was not found", HandlerResponseStatus.NotFound, command.GroupChatId.Value);
        }

        var member = group.Join(chatterId);
        if (member is null)
        {
            return ("This group chat is already joined", HandlerResponseStatus.Conflict, command.GroupChatId.Value);
        }

        _context.Add(member);

        var response = new GroupResponse<GroupChatJoinedMessage>
        {
            Method = ChatHubMethods.ReceiveGroupJoined,
            Message = new(group.Id.Value, chatterId.Value),
            ReceiverId = group.Id.Value.ToString()
        };

        return (response, HandlerResponseStatus.NoContent);
    }
}
