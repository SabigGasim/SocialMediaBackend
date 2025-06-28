using Dapper;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;
using System.Text.Json;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Messaging.Outbox;

public class ProcessOutboxMessagesCommandHandler(
    IDbConnectionFactory factory,
    Mediator.IMediator mediator)
    : ICommandHandler<ProcessOutboxMessagesCommand>
{
    private readonly IDbConnectionFactory _factory = factory;
    private readonly Mediator.IMediator _mediator = mediator;
    private readonly AsyncRetryPolicy _policy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
            [
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(3)
            ]);

    public async Task<HandlerResponse> ExecuteAsync(ProcessOutboxMessagesCommand command, CancellationToken ct)
    {
        using var connection = await _factory.CreateAsync(ct);

        const string sql = $"""
                       SELECT
                           m."Id" AS "{nameof(OutboxMessageDto.Id)}", 
                           m."Type" AS "{nameof(OutboxMessageDto.Type)}", 
                           m."Content" AS "{nameof(OutboxMessageDto.Content)}" 
                       FROM {Schema.Users}."OutboxMessages" AS m 
                       WHERE m."Processed" = FALSE AND m."Error" IS NULL
                       ORDER BY m."OccurredOn"
                       """;

        var messages = await connection.QueryAsync<OutboxMessageDto>(sql);

        var messagesList = messages.AsList();

        foreach (var message in messagesList)
        {
            var result = await _policy.ExecuteAndCaptureAsync(() => ProcessMessage(message));

            if (result.Outcome == OutcomeType.Successful)
            {
                const string updateOnSuccess = $"""
                    UPDATE {Schema.Users}."OutboxMessages"
                    SET "Processed" = TRUE, "ProcessedDate" = @NowDate
                    WHERE "Id" = @Id
                    """;

                await connection.ExecuteScalarAsync(updateOnSuccess, new
                {
                    message.Id,
                    NowDate = TimeProvider.System.GetUtcNow(),
                });

                continue;
            }

            const string updateOnErrorSql = $"""
                UPDATE {Schema.Users}."OutboxMessages"
                SET "ProcessedDate" = @NowDate, "Error" = @Error
                WHERE "Id" = @Id
                """;

            await connection.ExecuteScalarAsync(
                updateOnErrorSql,
                new
                {
                    message.Id,
                    NowDate = TimeProvider.System.GetUtcNow(),
                    Error = result.FinalException.ToString(),
                });
        }

        return HandlerResponseStatus.NoContent;
    }

    private async Task ProcessMessage(OutboxMessageDto outboxMessage)
    {
        var type = Type.GetType(outboxMessage.Type)!;

        var notification = JsonConvert.DeserializeObject(outboxMessage.Content, type)!;

        await _mediator.Publish(notification);
    }
}

class OutboxMessageDto
{
    public Guid Id { get; set; }
    public string Type { get; set; } = default!;
    public string Content { get; set; } = default!;
}