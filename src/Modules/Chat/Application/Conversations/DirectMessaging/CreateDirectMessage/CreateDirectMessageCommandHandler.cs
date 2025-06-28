using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.Modules.Chat.Application.Mappings;
using SocialMediaBackend.Modules.Chat.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectMessage;

internal sealed class CreateDirectMessageCommandHandler(
    ChatDbContext context,
    IAuthorizationHandler<DirectChat> authorizationHandler
    )
    : ISingleUserCommandHandler<CreateDirectMessageCommand, DirectMessageMessage, SendDirectMessageResponse>
{
    private readonly ChatDbContext _context = context;
    private readonly IAuthorizationHandler<DirectChat> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse<SingleUserResponse<DirectMessageMessage, SendDirectMessageResponse>>> ExecuteAsync(CreateDirectMessageCommand command, CancellationToken ct)
    {
        var chat = await _context.DirectChats.FindAsync([command.DirectChatId], ct);
        if (chat is null)
        {
            return ("You have to create a chat with the user before sending a message", HandlerResponseStatus.Conflict);
        }

        var senderId = new ChatterId(command.UserId);

        if (!_authorizationHandler.Authorize(senderId, chat))
        {
            return ("You're unauthorized to send messages in this chat", HandlerResponseStatus.Unauthorized);
        }

        var message = chat.AddMessage(senderId, command.Text);

        _context.Add(message);

        var recieverId = chat.GetReceiverId(senderId);

        var response = message.MapToResponse(recieverId, ChatHubMethods.SendDirectMessage);

        return (response, HandlerResponseStatus.Created);
    }
}
