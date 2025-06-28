using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.MarkGroupMessagAsSeen;

internal sealed class MarkGroupMessagAsSeenCommandHandler(
    IAuthorizationHandler<GroupChat, GroupChatId> authorizationHandler,
    IChatRepository chatRepository,
    IUserLockMangaer lockMangaer)
    : ICommandHandler<MarkGroupMessageAsSeenCommand, GroupMessageId?>
{
    private readonly IAuthorizationHandler<GroupChat, GroupChatId> _authorizationHandler = authorizationHandler;
    private readonly IChatRepository _chatRepository = chatRepository;
    private readonly IUserLockMangaer _lockMangaer = lockMangaer;

    public async Task<HandlerResponse<GroupMessageId?>> ExecuteAsync(MarkGroupMessageAsSeenCommand command, CancellationToken ct)
    {
        var chatterId = new ChatterId(command.UserId);

        var authorizationResult = await _authorizationHandler.AuthorizeAsync(chatterId, command.GroupChatId);

        if (!authorizationResult.IsSuccess)
        {
            return authorizationResult;
        }

        await using (await _lockMangaer.AcquireLockAsync(command.UserId.ToString(), $"MarkGroupMessage({command.GroupChatId.Value})"))
        {
            var messageId = await _chatRepository.MarkGroupMessagesAsSeenAsync(command.GroupChatId, chatterId);

            return messageId.HasValue
                ? (new GroupMessageId(messageId.Value), HandlerResponseStatus.OK)
                : (string.Empty, HandlerResponseStatus.NotModified);
        }
    }
}
