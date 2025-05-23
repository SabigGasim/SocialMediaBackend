﻿using Dapper;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Auth;

internal class GroupChatAuthorizationHandler(
    IDbConnectionFactory factory)
    : IAuthorizationHandler<GroupChat, GroupChatId>
{
    private readonly IDbConnectionFactory _factory = factory;

    public async Task<bool> AuthorizeAsync(ChatterId memberId, GroupChatId resourceId, CancellationToken ct = default)
    {
        const string sql = $"""
            SELECT EXISTS (
                SELECT 1
                FROM {Schema.Chat}."GroupChatMembers"
                WHERE "GroupChatId" = @GroupChatId
                  AND "MemberId" = @MemberId
            );
            """;

        using (var connection = await _factory.CreateAsync(ct))
        {
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(sql, new
            {
                MemberId = memberId.Value,
                GroupChatId = resourceId.Value
            }, cancellationToken: ct));
        }
    }
}
