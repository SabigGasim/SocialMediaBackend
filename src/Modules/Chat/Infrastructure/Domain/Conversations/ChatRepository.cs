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

    public async Task<IEnumerable<DirectMessageDto>> GetAllDirectChatMessages(
        ChatterId chatterId, 
        DirectChatId directChatId,
        int page,
        int pageSize,
        CancellationToken ct = default)
    {
        const string sql = $"""
            SELECT
              dm."Id"        AS "MessageId",
              dm."SenderId"  AS "SenderId",
              dm."Text"      AS "Text",
              dm."SentAt"    AS "SentAt",
              dm."Status"    AS "Status"
            FROM {Schema.Chat}."DirectMessages" dm

            -- find the UserDirectChat record for this chatter + chat
            JOIN {Schema.Chat}."UserDirectChats" udc
              ON udc."ChatterId"    = @{nameof(chatterId)}
             AND udc."DirectChatId" = @{nameof(directChatId)}

            -- ensure the message actually belongs to that user's chat-view
            JOIN {Schema.Chat}."UserDirectMessages" udm
              ON udm."DirectMessageId" = dm."Id"
             AND udm."UserDirectChatId" = udc."Id"

            WHERE dm."ChatId" = @{nameof(directChatId)}
            ORDER BY dm."Id"
            LIMIT  @{nameof(pageSize)}
            OFFSET @{nameof(page)};
            """;

        var parameters = new
        {
            chatterId = chatterId.Value,
            directChatId = directChatId.Value,
            pageSize,
            page = (page - 1) * pageSize
        };

        using (var connection = await _factory.CreateAsync(ct))
        {
            var messages = await connection.QueryAsync<DirectMessageDto>(new CommandDefinition(
                sql, parameters, cancellationToken: ct)
                );

            return messages;
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
