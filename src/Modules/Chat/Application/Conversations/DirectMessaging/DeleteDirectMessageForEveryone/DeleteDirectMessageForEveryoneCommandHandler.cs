using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.DeleteDirectMessageForEveryone;

internal sealed class DeleteDirectMessageForEveryoneCommandHandler(
    ChatDbContext context,
    IAuthorizationHandler<DirectChat> authorizationHandler,
    IChatterContext chatterContext)
    : ISingleUserCommandHandler<DeleteDirectMessageForEveryoneCommand, DeleteDirectMessageMessage>
{
    private readonly ChatDbContext _context = context;
    private readonly IAuthorizationHandler<DirectChat> _authorizationHandler = authorizationHandler;
    private readonly IChatterContext _chatterContext = chatterContext;

    public async Task<HandlerResponse<SingleUserResponse<DeleteDirectMessageMessage>>> ExecuteAsync(DeleteDirectMessageForEveryoneCommand command, CancellationToken ct)
    {
        var chat = await _context.DirectChats
            .Where(x => x.Id == command.ChatId)
            .Include(x => x.Messages.Where(x => x.Id == command.MessageId))
            .FirstOrDefaultAsync(ct);

        if (chat is null)
        {
            return ("Chat with the given Id was not found", HandlerResponseStatus.NotFound, command.ChatId.Value);
        }

        var chatterId = _chatterContext.ChatterId;

        if (!_authorizationHandler.Authorize(chatterId, chat))
        {
            return ("You're unauthorized to access this chat", HandlerResponseStatus.Unauthorized);
        }

        var result = chat.DeleteMessage(command.MessageId);

        if (!result.IsSuccess)
        {
            return result;
        }

        var receiverId = chat.GetReceiverId(chatterId);

        var response = new SingleUserResponse<DeleteDirectMessageMessage>
        {
            ReceiverId = receiverId.Value.ToString(),
            Message = new(command.MessageId.Value),
            Method = ChatHubMethods.DeleteDirectMessage
        };

        return (response, HandlerResponseStatus.NoContent);
    }
}
