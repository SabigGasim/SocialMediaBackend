using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.LeaveGroupChat;

internal sealed class LeaveGroupChatCommandHandler(ChatDbContext context, IChatterContext chatterContext)
    : IGroupCommandHandler<LeaveGroupChatCommand, GroupChatLeftMessage>
{
    private readonly ChatDbContext _context = context;
    private readonly IChatterContext _chatterContext = chatterContext;

    public async Task<HandlerResponse<GroupResponse<GroupChatLeftMessage>>> ExecuteAsync(LeaveGroupChatCommand command, CancellationToken ct)
    {
        var chatterId = _chatterContext.ChatterId;

        var group = await _context.GroupChats
            .Where(x => x.Id == command.GroupChatId)
            .Include(x => x.Members.Where(m => m.MemberId == chatterId))
            .FirstOrDefaultAsync(ct);

        if (group is null)
        {
            return ("Group with the given Id was not found", HandlerResponseStatus.NotFound, command.GroupChatId.Value);
        }

        var result = group.Leave(chatterId);
        if (!result.IsSuccess)
        {
            return result;
        }

        var response = new GroupResponse<GroupChatLeftMessage>
        {
            Method = ChatHubMethods.ReceiveGroupLeft,
            Message = new(group.Id.Value, chatterId.Value),
            ReceiverId = group.Id.Value.ToString()
        };

        return (response, HandlerResponseStatus.NoContent);
    }
}
