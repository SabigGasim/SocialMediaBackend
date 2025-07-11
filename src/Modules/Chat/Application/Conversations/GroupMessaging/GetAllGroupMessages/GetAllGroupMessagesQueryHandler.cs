﻿using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Application.Mappings;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.GetAllGroupMessages;

internal sealed class GetAllGroupMessagesQueryHandler(
    IChatRepository chatRepository,
    IAuthorizationHandler<GroupChat, GroupChatId> authorizationHandler)
    : IQueryHandler<GetAllGroupMessagesQuery, GetAllGroupMessagesResponse>
{
    private readonly IChatRepository _chatRepository = chatRepository;
    private readonly IAuthorizationHandler<GroupChat, GroupChatId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse<GetAllGroupMessagesResponse>> ExecuteAsync(GetAllGroupMessagesQuery query, CancellationToken ct)
    {
        var chatterId = new ChatterId(query.UserId);

        var authorizationResult = await _authorizationHandler.AuthorizeAsync(chatterId, query.GroupChatId, ct);

        if (!authorizationResult.IsSuccess)
        {
            return authorizationResult;
        }

        var messages = await _chatRepository
            .GetAllGroupChatMessages(chatterId, query.GroupChatId, query.Page, query.PageSize, ct);

        return messages.MapToResponse();
    }
}
