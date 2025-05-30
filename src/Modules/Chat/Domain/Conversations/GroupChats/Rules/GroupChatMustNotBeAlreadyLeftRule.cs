﻿using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Rules;
internal class GroupChatMustNotBeAlreadyLeftRule(bool isJoined) : IBusinessRule
{
    private readonly bool _isJoined = isJoined;

    public string Message { get; } = "User already left the group or hasn't joined yet";

    public bool IsBroken()
    {
        return !_isJoined;
    }

    public Task<bool> IsBrokenAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
