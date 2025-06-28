using Dapper;
using Polly;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.BuildingBlocks.Application;
using System.Text.Json;
using Autofac;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.InternalCommands;

internal sealed class ProcessInternalCommandsCommandHandler : ICommandHandler<ProcessInternalCommandsCommand>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public ProcessInternalCommandsCommandHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<HandlerResponse> ExecuteAsync(ProcessInternalCommandsCommand command, CancellationToken ct)
    {
        using var connection = await _dbConnectionFactory.CreateAsync(ct);

        const string sql = $"""
                           SELECT
                               c."Id" AS "{nameof(InternalCommandDto.Id)}", 
                               c."Type" AS "{nameof(InternalCommandDto.Type)}", 
                               c."Data" AS "{nameof(InternalCommandDto.Data)}" 
                           FROM "{Schema.Chat}"."InternalCommands" AS c 
                           WHERE c."Processed" = FALSE
                           ORDER BY c."EnqueueDate"
                           """;

        var commands = await connection.QueryAsync<InternalCommandDto>(sql);

        var internalCommandsList = commands.AsList();

        var policy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
            [
#if DEBUG
                TimeSpan.FromHours(1)
#else
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(3)
#endif
            ]);

        foreach (var internalCommand in internalCommandsList)
        {
            var result = await policy.ExecuteAndCaptureAsync(() => ProcessCommand(internalCommand));

            if (result.Outcome == OutcomeType.Successful)
            {
                continue;
            }

            const string updateOnErrorSql = $"""
                                            UPDATE {Schema.Chat}."InternalCommands"
                                            SET "ProcessedDate" = @NowDate, "Error" = @Error
                                            WHERE "Id" = @Id
                                            """;

            await connection.ExecuteScalarAsync(
                updateOnErrorSql,
                new
                {
                    internalCommand.Id,
                    NowDate = TimeProvider.System.GetUtcNow(),
                    Error = result.FinalException.ToString(),
                });
        }

        return HandlerResponseStatus.NoContent;
    }

    private static async Task ProcessCommand(InternalCommandDto internalCommand)
    {
        var type = Type.GetType(internalCommand.Type)!;

        dynamic commandToProcess = JsonSerializer.Deserialize(internalCommand.Data, type)!;

        await using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(type);
            var handler = (dynamic)scope.Resolve(handlerType);

            await handler.ExecuteAsync(commandToProcess, CancellationToken.None);
        }
    }

    private class InternalCommandDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = default!;
        public string Data { get; set; } = default!;
    }
}
