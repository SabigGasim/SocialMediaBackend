using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.DeleteDirectMessageForMe;

internal sealed class DeleteDirectMessageForMeCommandHandler(
    ChatDbContext context,
    IAuthorizationHandler<UserDirectChat> authorizationHandler,
    IChatterContext chatterContext) 
    : ICommandHandler<DeleteDirectMessageForMeCommand>
{
    private readonly ChatDbContext _context = context;
    private readonly IAuthorizationHandler<UserDirectChat> _authorizationHandler = authorizationHandler;
    private readonly IChatterContext _chatterContext = chatterContext;

    public async Task<HandlerResponse> ExecuteAsync(DeleteDirectMessageForMeCommand command, CancellationToken ct)
    {
        var chatterId = _chatterContext.ChatterId;

        var chat = await _context.UserDirectChats
            .Where(x => x.DirectChatId == command.DirectChatId)
            .Include(x => x.Messages.Where(x => x.DirectMessageId == command.MessageId))
            .FirstOrDefaultAsync(ct);

        if (chat is null)
        {
            return ("Chat with the given Id was not found", HandlerResponseStatus.NotFound, command.MessageId.Value);
        }

        if (!_authorizationHandler.Authorize(chatterId, chat))
        {
            return ("You're unauthorized to view this chat", HandlerResponseStatus.Unauthorized);
        }

        chat.DeleteMessage(command.MessageId);

        return HandlerResponseStatus.NoContent;
    }
}
