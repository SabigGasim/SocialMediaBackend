using Dapper;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.Follows.UnfollowChatter;

public class UnfollowChatterCommandHandler(IDbConnectionFactory factory) : ICommandHandler<UnfollowChatterCommand>
{
    private readonly IDbConnectionFactory _factory = factory;

    public async Task<HandlerResponse> ExecuteAsync(UnfollowChatterCommand command, CancellationToken ct)
    {
        var parameters = new
        {
            FollowerId = command.FollowerId.Value,
            ChatterId = command.ChatterId.Value
        };

        const string sql = $"""
            BEGIN;

            DELETE FROM {Schema.Chat}."Follows"
                WHERE "FollowerId" = @{nameof(parameters.FollowerId)}
                AND "FollowingId" = @{nameof(parameters.ChatterId)};

            UPDATE {Schema.Chat}."Chatters"
                SET "FollowersCount" = "FollowersCount" - 1
                WHERE "Id" = @{nameof(command.ChatterId)};
            
            UPDATE {Schema.Chat}."Chatters"
                SET "FollowingCount" = "FollowingCount" - 1
                WHERE "Id" = @{nameof(command.FollowerId)};

            COMMIT;
            """;

        using (var connection = await _factory.CreateAsync())
        {
            await connection.ExecuteAsync(sql, parameters);
        }

        return HandlerResponseStatus.NoContent;
    }
}
