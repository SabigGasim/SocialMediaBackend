using Dapper;
using Newtonsoft.Json;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.InternalCommands;

public class CommandsScheduler(IDbConnectionFactory connectionFactory) : ICommandsScheduler
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public async ValueTask EnqueueAsync<TInternalCommand>(TInternalCommand command, string? idempotencyKey = null)
        where TInternalCommand : InternalCommandBase
    {
        using (var connection = await _connectionFactory.CreateAsync())
        {
            const string sqlInsert =
                $"""
                INSERT INTO {Schema.AppSubscriptions}."InternalCommands" ("Id", "EnqueueDate" , "Type", "Data", "IdempotencyKey")
                VALUES (@Id, @EnqueueDate, @Type, @Data, @IdempotencyKey)
                """;

            await connection.ExecuteAsync(sqlInsert, new
            {
                command.Id,
                EnqueueDate = TimeProvider.System.GetUtcNow(),
                Type = command.GetType().AssemblyQualifiedName,
                Data = JsonConvert.SerializeObject(command),
                IdempotencyKey = idempotencyKey
            });
        }
    }
}

