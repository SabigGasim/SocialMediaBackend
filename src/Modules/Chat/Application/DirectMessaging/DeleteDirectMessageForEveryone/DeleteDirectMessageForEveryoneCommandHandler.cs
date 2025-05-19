using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.DirectMessaging.DeleteDirectMessageForEveryone;

public class DeleteDirectMessageForEveryoneCommandHandler : ICommandHandler<DeleteDirectMessageForEveryoneCommand>
{
    private readonly ChatDbContext _context;
    private readonly IAuthorizationHandler<DirectChat> _authorizationHandler;

    public DeleteDirectMessageForEveryoneCommandHandler(
        ChatDbContext context,
        IAuthorizationHandler<DirectChat> authorizationHandler)
    {
        _context = context;
        _authorizationHandler = authorizationHandler;
    }

    public async Task<HandlerResponse> ExecuteAsync(DeleteDirectMessageForEveryoneCommand command, CancellationToken ct)
    {
        var chat = await _context.DirectChats
            .Where(x => x.Id == command.ChatId)
            .Include(x => x.Messages.Where(x => x.Id == command.MessageId))
            .FirstOrDefaultAsync(ct);

        if (chat is null)
        {
            return ("Chat with the given Id was not found", HandlerResponseStatus.NotFound, command.ChatId.Value);
        }

        var chatterId = new ChatterId(command.UserId);

        if (!_authorizationHandler.Authorize(chatterId, chat))
        {
            return ("You're unauthorized to access this chat", HandlerResponseStatus.Unauthorized);
        }

        var deleted = chat.DeleteMessage(command.MessageId);
        
        return deleted
            ? HandlerResponseStatus.NoContent
            : ("Message with the given Id was not found",  HandlerResponseStatus.NotFound, command.MessageId.Value);
    }
}
