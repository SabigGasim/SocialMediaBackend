using Dapper;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.UpdateAuthorInfo;
internal sealed class UpdateAuthorInfoCommandHandler(
    IDbConnectionFactory factory) : ICommandHandler<UpdateAuthorInfoCommand>
{
    private readonly IDbConnectionFactory _factory = factory;

    public async Task<HandlerResponse> ExecuteAsync(UpdateAuthorInfoCommand command, CancellationToken ct)
    {
        const string sql = $"""
                UPDATE {Schema.Feed}."Authors"
                SET
                    "Username" = @{nameof(command.Username)},
                    "Nickname" = @{nameof(command.Nickname)},
                    "ProfileIsPublic" = @{nameof(command.ProfileIsPublic)},
                    "ProfilePictureUrl" = @{nameof(command.ProfilePicture.Url)},
                    "ProfilePictureFilePath" = @{nameof(command.ProfilePicture.FilePath)},
                    "ProfilePictureMediaType" = @{nameof(command.ProfilePicture.MediaType)}
                WHERE "Id" = @{nameof(command.AuthorId)};
                """;

        using (var connection = await _factory.CreateAsync())
        {
            await connection.ExecuteAsync(sql, new
            {
                command.AuthorId,
                command.Username,
                command.Nickname,
                command.ProfileIsPublic,
                command.ProfilePicture.Url,
                command.ProfilePicture.FilePath,
                command.ProfilePicture.MediaType
            });
        }

        return HandlerResponseStatus.NoContent;
    }
}
