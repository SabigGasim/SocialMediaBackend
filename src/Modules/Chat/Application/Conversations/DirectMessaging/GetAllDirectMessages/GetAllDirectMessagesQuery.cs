﻿using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.GetAllDirectMessages;

[HasPermission(Permissions.GetAllDirectMessages)]
public sealed class GetAllDirectMessagesQuery(Guid chatId, int page, int pageSize)
    : QueryBase<GetAllDirectMessagesResponse>, IUserRequest
{
    public Guid UserId { get; private set; }
    public bool IsAdmin { get; private set; }
    public DirectChatId DirectChatId { get; } = new(chatId);
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
