using Dapper;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Feed.Domain.Follows;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.Follows.FollowAuthor;
public class FollowAuthorCommandHandler(IDbConnectionFactory factory): ICommandHandler<FollowAuthorCommand>
{
    private readonly IDbConnectionFactory _factory = factory;

    public async Task<HandlerResponse> ExecuteAsync(FollowAuthorCommand command, CancellationToken ct)
    {
        var parameters = new
        {
            FollowerId = command.FollowerId.Value,
            AuthorId = command.AuthorId.Value,
            command.FollowedAt,
            Status = FollowStatus.Following
        };

        const string sql = $"""
            BEGIN;

            INSERT INTO {Schema.Feed}."Follows" (
                "FollowerId",
                "FollowingId",
                "FollowedAt",
                "Status"
            ) VALUES (
                @{nameof(parameters.FollowerId)},
                @{nameof(parameters.AuthorId)},
                @{nameof(parameters.FollowedAt)},
                @{nameof(parameters.Status)}
            );
            
            UPDATE {Schema.Feed}."Authors"
                SET "FollowersCount" = "FollowersCount" + 1
                WHERE "Id" = @{nameof(parameters.AuthorId)};
            
            UPDATE {Schema.Feed}."Authors"
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
