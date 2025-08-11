using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.DeleteGroupMessageForEveryone;

internal sealed class DeleteGroupMessageForEveryoneCommandHandler(
    ChatDbContext context,
    IAuthorizationHandler<GroupChat, GroupChatId> authorizationHandler,
    IChatterContext chatterContext)
    : IGroupCommandHandler<DeleteGroupMessageForEveryoneCommand, DeleteGroupMessageMessage>
{
    private readonly ChatDbContext _context = context;
    private readonly IAuthorizationHandler<GroupChat, GroupChatId> _authorizationHandler = authorizationHandler;
    private readonly IChatterContext _chatterContext = chatterContext;

    public async Task<HandlerResponse<GroupResponse<DeleteGroupMessageMessage>>> ExecuteAsync(DeleteGroupMessageForEveryoneCommand command, CancellationToken ct)
    {
        var chatterId = _chatterContext.ChatterId;

        var authorizationResult = await _authorizationHandler.AuthorizeAsync(chatterId, command.ChatId, ct);

        if (!authorizationResult.IsSuccess)
        {
            return authorizationResult;
        }

        var chat = await _context.GroupChats
            .Include(x => x.Members)
            .Include(x => x.Messages.Where(x => x.Id == command.MessageId))
            .Where(x => x.Id == command.ChatId)
            .FirstAsync(ct);

        var message = chat.Messages.FirstOrDefault();
        if (message is null)
        {
            return ("Message with the given Id was not found", HandlerResponseStatus.NotFound);
        }

        if (message.SenderId != chatterId)
        {
            return ("You can't delete other's messages", HandlerResponseStatus.Unauthorized);
        }

        chat.DeleteMessage(command.MessageId);

        var response = new GroupResponse<DeleteGroupMessageMessage>
        {
            Method = ChatHubMethods.DeleteGroupMessage,
            Message = new(message.Id.Value),
            ReceiverId = chat.Id.ToString()
        };

        return (response, HandlerResponseStatus.NoContent);
    }
}
