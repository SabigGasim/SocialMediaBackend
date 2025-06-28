using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.MarkDirectMessageAsSeen;

internal sealed class MarkDirectMessageAsSeenCommandHandler(
    IAuthorizationHandler<DirectChat, DirectChatId> authorizationHandler,
    IChatRepository chatRepository)
    : ICommandHandler<MarkDirectMessageAsSeenCommand>
{
    private readonly IAuthorizationHandler<DirectChat, DirectChatId> _authorizationHandler = authorizationHandler;
    private readonly IChatRepository _chatRepository = chatRepository;

    public async Task<HandlerResponse> ExecuteAsync(MarkDirectMessageAsSeenCommand command, CancellationToken ct)
    {
        var chatterId = new ChatterId(command.UserId);

        var authorizationResult = await _authorizationHandler.AuthorizeAsync(chatterId, command.DirectChatId);

        if (!authorizationResult.IsSuccess)
        {
            return authorizationResult;
        }

        await _chatRepository.MarkDirectMessageAsSeenAsync(command.DirectChatId, command.DirectMessageId);

        return HandlerResponseStatus.Modified;
    }
}
