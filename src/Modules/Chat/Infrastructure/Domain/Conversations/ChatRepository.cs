using Dapper;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations;

internal class ChatRepository(IDbConnectionFactory factory) : IChatRepository
{
    private readonly IDbConnectionFactory _factory = factory;

    public async Task<bool> DirectChatExistsAsync(ChatterId firstChatterId, ChatterId secondChatterId, CancellationToken ct = default)
    {
        const string sql = $"""
            SELECT EXISTS (
                SELECT 1
                FROM {Schema.Chat}."DirectChats"
                WHERE ("FirstChatterId" = @{nameof(firstChatterId)} AND "SecondChatterId" = @{nameof(secondChatterId)})
                   OR ("FirstChatterId" = @{nameof(secondChatterId)} AND "SecondChatterId" = @{nameof(firstChatterId)})
            );
            """;

        using (var connection = await _factory.CreateAsync(ct))
        {
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(
                sql, new
                {
                    firstChatterId,
                    secondChatterId
                },
                cancellationToken: ct));
        }
    }

    public async Task<bool> ExistsAsync(DirectChatId chatId, CancellationToken ct = default)
    {
        const string sql = $"""
            SELECT EXISTS (
                SELECT 1
                FROM {Schema.Chat}."DirectChats"
                WHERE "Id" = @{nameof(chatId)}
            );
            """;

        using (var connection = await _factory.CreateAsync(ct))
        {
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(
                sql, new
                {
                    chatId
                },
                cancellationToken: ct));
        }
    }

    public async Task<bool> ExistsAsync(GroupChatId chatId, CancellationToken ct = default)
    {
        const string sql = $"""
            SELECT EXISTS (
                SELECT 1
                FROM {Schema.Chat}."GroupChats"
                WHERE "Id" = {nameof(chatId)}
            );
            """;

        using (var connection = await _factory.CreateAsync(ct))
        {
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(
                sql, new
                {
                    chatId
                },
                cancellationToken: ct));
        }
    }

    public async Task<bool> ExistsAsync(UserGroupChatId chatId, CancellationToken ct = default)
    {
        const string sql = $"""
            SELECT EXISTS (
                SELECT 1
                FROM {Schema.Chat}."UserGroupChats"
                WHERE "Id" = {nameof(chatId)}
            );
            """;

        using (var connection = await _factory.CreateAsync(ct))
        {
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(
                sql, new
                {
                    chatId
                },
                cancellationToken: ct));
        }
    }

    public async Task<bool> ExistsAsync(UserDirectChatId chatId, CancellationToken ct = default)
    {
        const string sql = $"""
            SELECT EXISTS (
                SELECT 1
                FROM {Schema.Chat}."UserDirectChats"
                WHERE "Id" = {nameof(chatId)}
            );
            """;

        using (var connection = await _factory.CreateAsync(ct))
        {
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(
                sql, new
                {
                    chatId
                },
                cancellationToken: ct));
        }
    }
}
