using Dapper;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.MarkGroupMessagAsSeen;

public class MarkGroupMessagAsSeenCommandHandler(
    IAuthorizationHandler<GroupChat, GroupChatId> authorizationHandler,
    IDbConnectionFactory factory)
    : ICommandHandler<MarkGroupMessageAsSeenCommand>
{
    private readonly IAuthorizationHandler<GroupChat, GroupChatId> _authorizationHandler = authorizationHandler;
    private readonly IDbConnectionFactory _factory = factory;

    public async Task<HandlerResponse> ExecuteAsync(MarkGroupMessageAsSeenCommand command, CancellationToken ct)
    {
        var chatterId = new ChatterId(command.UserId);

        if (!await _authorizationHandler.AuthorizeAsync(chatterId, command.GroupChatId))
        {
            return ("You're unauthorized to view this group", HandlerResponseStatus.Unauthorized);
        }

        using (var connection = await _factory.CreateAsync())
        {
            const string sql = $"""
                INSERT INTO {Schema.Chat}."GroupMessage_SeenBy" ("GroupMessageId", "ChatterId")
                VALUES (@MessageId, @ChatterId);
                """;

            await connection.ExecuteAsync(sql, new
            {
                MessageId = command.GroupMessageId,
                ChatterId = chatterId
            });
        }

        return HandlerResponseStatus.NoContent;
    }
}
