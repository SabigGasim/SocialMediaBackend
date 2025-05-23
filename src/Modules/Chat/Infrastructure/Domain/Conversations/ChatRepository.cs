using Dapper;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;
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
                    firstChatterId = firstChatterId.Value,
                    secondChatterId = secondChatterId.Value
                },
                cancellationToken: ct));
        }
    }

    public async Task<IEnumerable<string>> GetJoinedUserGroupChats(ChatterId chatterId, CancellationToken ct = default)
    {
        const string sql = $"""
            SELECT "GroupChatId"::TEXT
            FROM {Schema.Chat}."UserGroupChats"
            WHERE "ChatterId" = @ChatterId
              AND "IsJoined" = TRUE;
            """;

        using (var connection = await _factory.CreateAsync(ct))
        {
            return await connection.QueryAsync<string>(new CommandDefinition(sql, new
            {
                ChatterId = chatterId.Value
            }, cancellationToken: ct));
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
            OFFSET @OFFSET;
            """;

        var parameters = new
        {
            chatterId = chatterId.Value,
            directChatId = directChatId.Value,
            pageSize,
            OFFSET = (page - 1) * pageSize
        };

        using (var connection = await _factory.CreateAsync(ct))
        {
            var messages = await connection.QueryAsync<DirectMessageDto>(new CommandDefinition(
                sql, parameters, cancellationToken: ct)
                );

            return messages;
        }
    }

    public async Task<IEnumerable<string>> GetChattersWithDirectOrGroupChatWith(ChatterId chatterId)
    {
        const string sql = $"""
            WITH DirectChatMembers AS (
                SELECT 
                    CASE 
                        WHEN "FirstChatterId" = @ChatterId THEN "SecondChatterId"
                        ELSE "FirstChatterId"
                    END AS "ChatterId"
                FROM {Schema.Chat}."DirectChats"
                WHERE "FirstChatterId" = @ChatterId OR "SecondChatterId" = @ChatterId
            ),
            GroupChatMembers AS (
                SELECT DISTINCT "Member"."ChatterId"
                FROM {Schema.Chat}."GroupChat_Chatters" AS "Member"
                INNER JOIN {Schema.Chat}."GroupChat_Chatters" AS "Target"
                    ON "Member"."GroupChatId" = "Target"."GroupChatId"
                WHERE "Target"."ChatterId" = @ChatterId
                  AND "Member"."ChatterId" != @ChatterId
            )
            SELECT DISTINCT "ChatterId"::TEXT
            FROM DirectChatMembers
            UNION
            SELECT DISTINCT "ChatterId"::TEXT
            FROM GroupChatMembers;
            """;

        using (var connection = await _factory.CreateAsync())
        {
            return await connection.QueryAsync<string>(sql, new { ChatterId = chatterId.Value });
        }
    }

    public async Task<string?> GetReceiverIdAsync(DirectChatId chatId, ChatterId senderId, CancellationToken ct = default)
    {
        const string sql = $"""
            SELECT
              CASE
                WHEN "FirstChatterId" = @SenderId THEN "SecondChatterId"
                WHEN "SecondChatterId" = @SenderId THEN "FirstChatterId"
                ELSE NULL
              END AS "ReceiverId"::TEXT
            FROM {Schema.Chat}."DirectChats"
            WHERE "Id" = @ChatId
            """;

        using (var connection = await _factory.CreateAsync())
        {
            return await connection.QuerySingleOrDefaultAsync<string>(new CommandDefinition(sql, new
            {
                SenderId = senderId.Value,
                ChatId = chatId.Value,
            }, cancellationToken: ct));
        }
    }

    public async Task MarkDirectMessageAsSeenAsync(DirectChatId chatId, DirectMessageId lastSeenMessageId)
    {
        const string sql = $"""
            UPDATE {Schema.Chat}."DirectMessages"
            SET "Status" = @SeenStatus
            WHERE "DirectChatId" = @ChatId
              AND "Id" < @LastSeenMessageId;
            """;

        using (var connection = await _factory.CreateAsync())
        {
            await connection.ExecuteAsync(sql, new
            {
                ChatId = chatId.Value,
                LastSeenMessageId = lastSeenMessageId.Value,
                SeenStatus = MessageStatus.Seen,
            });
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

    public async Task<IEnumerable<GroupMessageDto>> GetAllGroupChatMessages(ChatterId chatterId, GroupChatId groupChatId, int page, int pageSize, CancellationToken ct)
    {
        const string sql = $"""
            SELECT
              gm."Id"        AS "MessageId",
              gm."SenderId"  AS "SenderId",
              gm."Text"      AS "Text",
              gm."SentAt"    AS "SentAt"
            FROM {Schema.Chat}."GroupMessage" gm

            -- find the UserGroupChat record for this chatter + chat
            JOIN {Schema.Chat}."UserGroupChats" ugc
              ON ugc."ChatterId"   = @{nameof(chatterId)}
             AND ugc."GroupChatId" = @{nameof(groupChatId)}

            -- ensure the message actually belongs to that user's chat-view
            JOIN {Schema.Chat}."UserGroupMessages" ugm
              ON ugm."GroupMessageId" = gm."Id"
             AND ugm."UserGroupChatId" = ugm."Id"

            WHERE gm."GroupChatId" = @{nameof(groupChatId)}
            ORDER BY gm."Id"
            LIMIT  @{nameof(pageSize)}
            OFFSET @OFFSET;
            """;

        var parameters = new
        {
            chatterId = chatterId.Value,
            groupChatId = groupChatId.Value,
            pageSize,
            OFFSET = (page - 1) * pageSize
        };

        using (var connection = await _factory.CreateAsync(ct))
        {
            var messages = await connection.QueryAsync<GroupMessageDto>(new CommandDefinition(
                sql, parameters, cancellationToken: ct)
                );

            return messages;
        }
    }
}
