using Dapper;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.DeleteGroupMessageForMe;

internal sealed class DeleteGroupMessageForMeCommandHandler(IDbConnectionFactory factory)
    : ICommandHandler<DeleteGroupMessageForMeCommand>
{
    private readonly IDbConnectionFactory _factory = factory;

    public async Task<HandlerResponse> ExecuteAsync(DeleteGroupMessageForMeCommand command, CancellationToken ct)
    {
        const string sql = $"""
            DELETE FROM {Schema.Chat}."UserGroupMessages" AS ugm
            USING "GroupMessages" AS gm
            WHERE ugm."GroupMessageId" = gm."Id"
              AND ugm."GroupMessageId" = @MessageId
              AND gm."SenderId" = @SenderId
              AND gm."GroupChatId" = @GroupChatId;
            """;

        using (var connection = await _factory.CreateAsync(ct))
        {
            var result = await connection.ExecuteAsync(new CommandDefinition(sql, new
            {
                MessageId = command.GroupMessageId.Value,
                GroupChatId = command.GroupChatId.Value,
                SenderId = command.UserId
            }, cancellationToken: ct));

            return result == 1
                ? HandlerResponseStatus.NoContent
                : ("Message was not found in the specified group", HandlerResponseStatus.NotFound, command.GroupMessageId.Value);
        }
    }
}
