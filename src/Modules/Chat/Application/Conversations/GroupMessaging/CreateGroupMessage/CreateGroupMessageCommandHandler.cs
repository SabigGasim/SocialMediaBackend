using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.Modules.Chat.Application.Mappings;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupMessage;

public class CreateGroupMessageCommandHandler(
    ChatDbContext context,
    IAuthorizationHandler<GroupChat, GroupChatId> authorizationHandler)
    : IMultipleUsersCommandHandler<CreateGroupMessageCommand, CreateGroupMessageMessage, SendGroupMessageResponse>
{
    private readonly ChatDbContext _context = context;
    private readonly IAuthorizationHandler<GroupChat, GroupChatId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse<MultipleUsersResponse<CreateGroupMessageMessage, SendGroupMessageResponse>>> ExecuteAsync(CreateGroupMessageCommand command, CancellationToken ct)
    {
        var senderId = new ChatterId(command.UserId);

        var authorizationResult = await _authorizationHandler.AuthorizeAsync(senderId, command.ChatId, ct);

        if (!authorizationResult.IsSuccess)
        {
            return authorizationResult;
        }

        var groupChat = await _context.GroupChats.FirstAsync(x => x.Id == command.ChatId, ct);

        var message = groupChat.AddMessage(senderId, command.Text);

        _context.Add(message);

        var response = message.MapToCreateResponse(groupChat);

        return (response, HandlerResponseStatus.Created);
    }
}
