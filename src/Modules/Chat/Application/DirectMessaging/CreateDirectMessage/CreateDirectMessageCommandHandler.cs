using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Application.Mappings;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.DirectMessaging.SendDirectMessage;

public class CreateDirectMessageCommandHandler(ChatDbContext context) : ICommandHandler<CreateDirectMessageCommand, SendDirectMessageResponse>
{
    private readonly ChatDbContext _context = context;

    private static HandlerResponse<SendDirectMessageResponse> DirectChatNotFoundError => ("You have to create a chat with the user before sending a message", HandlerResponseStatus.Conflict);

    public async Task<HandlerResponse<SendDirectMessageResponse>> ExecuteAsync(CreateDirectMessageCommand command, CancellationToken ct)
    {
        var chat = await _context.DirectChats.FindAsync([command.DirectChatId], ct);
        if (chat is null)
        {
            return DirectChatNotFoundError;
        }

        var senderId = new ChatterId(command.UserId);
        
        var message = chat.AddMessage(senderId, command.Text);
        if (message is null)
        {
            return DirectChatNotFoundError;
        }

        _context.Add(message);

        return (message.MapToResponse(), HandlerResponseStatus.Created);
    }
}
