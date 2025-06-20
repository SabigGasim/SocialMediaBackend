﻿using Dapper;
using Newtonsoft.Json;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.InternalCommands;

public class CommandsScheduler(IDbConnectionFactory connectionFactory) : ICommandsScheduler
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public async ValueTask EnqueueAsync<TInternalCommand>(TInternalCommand command)
        where TInternalCommand : InternalCommandBase
    {
        using (var connection = await _connectionFactory.CreateAsync())
        {
            const string sqlInsert =
                $"""
                INSERT INTO {Schema.Payments}."InternalCommands" ("Id", "EnqueueDate" , "Type", "Data")
                VALUES (@Id, @EnqueueDate, @Type, @Data)
                """;

            await connection.ExecuteAsync(sqlInsert, new
            {
                command.Id,
                EnqueueDate = TimeProvider.System.GetUtcNow(),
                Type = $"{command.GetType().FullName}, {command.GetType().Assembly.FullName}",
                Data = JsonConvert.SerializeObject(command)
            });
        }
    }
}

