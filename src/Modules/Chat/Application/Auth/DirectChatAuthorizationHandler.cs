using Dapper;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Auth;

internal class DirectChatAuthorizationHandler : IAuthorizationHandler<DirectChat>
{
    public bool Authorize(ChatterId? chatterId, DirectChat resource)
    {
        return chatterId == resource.FirstChatterId
            || chatterId == resource.SecondChatterId;
    }
}

internal class DirectChatByIdAuthorizationHandler(IDbConnectionFactory factory) 
    : IAuthorizationHandler<DirectChat, DirectChatId>
{
    private readonly IDbConnectionFactory _factory = factory;

    public async Task<bool> AuthorizeAsync(ChatterId chatterId, DirectChatId chatId, CancellationToken ct = default)
    {
        const string sql = $"""
            SELECT CASE
              WHEN d."Id" IS NULL
                THEN {ResourceStatus.DoesntExists}
              WHEN @ChatterId = d."FirstChatterId" OR @ChatterId = d."SecondChatterId"
                THEN {ResourceStatus.ExistsAndAuthorized}
              ELSE {ResourceStatus.ExistsAndUnauthorized}
            END AS result
            FROM (
              SELECT "Id", "FirstChatterId", "SecondChatterId"
              FROM {Schema.Chat}."DirectChats"
              WHERE "Id" = @ChatId
            ) d
            LIMIT 1;
            """;

        using (var connection = await _factory.CreateAsync(ct))
        {
            var result = await connection
                .ExecuteScalarAsync<string>(new CommandDefinition(sql, new
                {
                    ChatId = chatId.Value,
                    ChatterId = chatterId.Value
                },
                cancellationToken: ct));

            return result == ResourceStatus.ExistsAndAuthorized;
        }
    }
}

file class ResourceStatus
{
    public const string DoesntExists = "0";
    public const string ExistsAndUnauthorized = "1";
    public const string ExistsAndAuthorized = "2";
}