﻿using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.DeleteGroupMessageForEveryone;

[HasPermission(Permissions.DeleteGroupMessageForEveryone)]
public sealed class DeleteGroupMessageForEveryoneCommand(Guid chatId, Guid messageId)
    : GroupCommandBase<DeleteGroupMessageMessage>, IUserRequest
{
    public Guid UserId { get; private set; }

    public bool IsAdmin { get; private set; }

    public GroupChatId ChatId { get; } = new(chatId);
    public GroupMessageId MessageId { get; } = new(messageId);

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
