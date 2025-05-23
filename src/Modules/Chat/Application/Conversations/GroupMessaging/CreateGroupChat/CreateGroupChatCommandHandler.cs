using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Application.Mappings;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupChat;

public class CreateGroupChatCommandHandler(ChatDbContext context)
    : IMultipleUsersCommandHandler<CreateGroupChatCommand, CreateGroupChatMessage, CreateGroupChatResponse>
{
    private readonly ChatDbContext _context = context;

    public async Task<HandlerResponse<MultipleUsersResponse<CreateGroupChatMessage, CreateGroupChatResponse>>> ExecuteAsync(CreateGroupChatCommand command, CancellationToken ct)
    {
        var ownerId = new ChatterId(command.UserId);

        var requestedMemberIds = command.MemberIds
            .Select(x => new ChatterId(x))
            .ToArray();

        var members = await _context.Chatters
            .Where(x => x.Id == ownerId || requestedMemberIds.Contains(ownerId))
            .ToArrayAsync(ct);

        var groupChat = GroupChat.Create(ownerId, command.GroupName, members.Select(x => x.Id));

        _context.GroupChats.Add(groupChat);

        var response = groupChat.MapToCreateResponse(requestedMemberIds, members);

        return (response, HandlerResponseStatus.Created);
    }
}
