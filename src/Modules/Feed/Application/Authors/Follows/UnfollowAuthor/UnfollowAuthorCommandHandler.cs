using Dapper;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.Follows.UnfollowAuthor;

public class UnfollowAuthorCommandHandler(IDbConnectionFactory factory) : ICommandHandler<UnfollowAuthorCommand>
{
    private readonly IDbConnectionFactory _factory = factory;

    public async Task<HandlerResponse> ExecuteAsync(UnfollowAuthorCommand command, CancellationToken ct)
    {
        var parameters = new
        {
            FollowerId = command.FollowerId.Value,
            AuthorId = command.AuthorId.Value
        };

        const string sql = $"""
            BEGIN;

            DELETE FROM {Schema.Feed}."Follows"
                WHERE "FollowerId" = @{nameof(parameters.FollowerId)}
                AND "FollowingId" = @{nameof(parameters.AuthorId)};

            UPDATE {Schema.Feed}."Authors"
                SET "FollowersCount" = "FollowersCount" - 1
                WHERE "Id" = @{nameof(command.AuthorId)};
            
            UPDATE {Schema.Feed}."Authors"
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
