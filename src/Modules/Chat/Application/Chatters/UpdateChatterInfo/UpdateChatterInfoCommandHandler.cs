using Dapper;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.UpdateChatterInfo;
internal sealed class UpdateChatterInfoCommandHandler(
    IDbConnectionFactory factory) : ICommandHandler<UpdateChatterInfoCommand>
{
    private readonly IDbConnectionFactory _factory = factory;

    public async Task<HandlerResponse> ExecuteAsync(UpdateChatterInfoCommand command, CancellationToken ct)
    {
        const string sql = $"""
                UPDATE {Schema.Chat}."Chatters"
                SET
                    "Username" = @{nameof(command.Username)},
                    "Nickname" = @{nameof(command.Nickname)},
                    "ProfileIsPublic" = @{nameof(command.ProfileIsPublic)},
                    "ProfilePictureUrl" = @{nameof(command.ProfilePicture.Url)},
                    "ProfilePictureFilePath" = @{nameof(command.ProfilePicture.FilePath)},
                    "ProfilePictureMediaType" = @{nameof(command.ProfilePicture.MediaType)}
                WHERE "Id" = @{nameof(command.ChatterId)};
                """;

        using (var connection = await _factory.CreateAsync())
        {
            await connection.ExecuteAsync(sql, new
            {
                command.ChatterId,
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
