using Dapper;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Domain.Follows;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.Follows.FollowChatter;
public class FollowChatterCommandHandler(IDbConnectionFactory factory): ICommandHandler<FollowChatterCommand>
{
    private readonly IDbConnectionFactory _factory = factory;

    public async Task<HandlerResponse> ExecuteAsync(FollowChatterCommand command, CancellationToken ct)
    {
        var parameters = new
        {
            FollowerId = command.FollowerId.Value,
            ChatterId = command.ChatterId.Value,
            command.FollowedAt,
            Status = FollowStatus.Following
        };

        const string sql = $"""
            BEGIN;

            INSERT INTO {Schema.Chat}."Follows" (
                "FollowerId",
                "FollowingId",
                "FollowedAt",
                "Status"
            ) VALUES (
                @{nameof(parameters.FollowerId)},
                @{nameof(parameters.ChatterId)},
                @{nameof(parameters.FollowedAt)},
                @{nameof(parameters.Status)}
            );
            
            UPDATE {Schema.Chat}."Chatters"
                SET "FollowersCount" = "FollowersCount" + 1
                WHERE "Id" = @{nameof(parameters.ChatterId)};
            
            UPDATE {Schema.Chat}."Chatters"
                SET "FollowingCount" = "FollowingCount" + 1
                WHERE "Id" = @{nameof(parameters.FollowerId)};
            
            COMMIT;

            """;

        using (var connection = await _factory.CreateAsync())
        {
            await connection.ExecuteAsync(sql, parameters);
        }

        return HandlerResponseStatus.NoContent;
    }
}
