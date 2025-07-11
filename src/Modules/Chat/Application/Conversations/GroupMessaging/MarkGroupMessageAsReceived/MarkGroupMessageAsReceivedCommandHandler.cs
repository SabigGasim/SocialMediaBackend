﻿using Microsoft.Extensions.Logging;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.MarkGroupMessageAsReceived;

internal sealed class MarkGroupMessageAsReceivedCommandHandler(
    IChatRepository repository,
    IAuthorizationHandler<GroupChat, GroupChatId> authorizationHandler,
    IUserLockMangaer lockMangaer)
    : ICommandHandler<MarkGroupMessageAsReceivedCommand>
{
    private readonly IChatRepository _repository = repository;
    private readonly IAuthorizationHandler<GroupChat, GroupChatId> _authorizationHandler = authorizationHandler;
    private readonly IUserLockMangaer _lockMangaer = lockMangaer;

    public async Task<HandlerResponse> ExecuteAsync(MarkGroupMessageAsReceivedCommand command, CancellationToken ct)
    {
        var chatterId = new ChatterId(command.UserId);

        var authorizationResult = await _authorizationHandler.AuthorizeAsync(chatterId, command.GroupChatId, ct);

        if (!authorizationResult.IsSuccess)
        {
            return authorizationResult;
        }

        await using (await _lockMangaer.AcquireLockAsync(command.UserId.ToString(), $"MarkGroupMessage({command.GroupChatId.Value})"))
        {
            await _repository.MarkGroupMessageAsReceivedAsync(command.GroupChatId, chatterId, command.MessageId);

            return HandlerResponseStatus.NoContent;
        }
    }
}
