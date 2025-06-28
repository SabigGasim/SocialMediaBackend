using Dapper;
using Newtonsoft.Json;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Messaging.Inbox;

internal sealed class InboxIntegrationEventHandler<TEvent>(IDbConnectionFactory factory) 
    : IIntegrationEventHandler<TEvent> where TEvent : IntegrationEvent
{
    private readonly IDbConnectionFactory _factory = factory;

    public async ValueTask Handle(TEvent notification, CancellationToken cancellationToken)
    {
        const string sqlInsert =
                $"""
                INSERT INTO {Schema.Feed}."InboxMessages" ("Id", "Type" , "Content", "OccurredOn", "Processed")
                VALUES (@Id, @Type, @Content, @OccurredOn, @Processed)
                """;

        using (var connection = await _factory.CreateAsync(CancellationToken.None))
        {
            await connection.ExecuteAsync(sqlInsert, new
            {
                Id = notification.Id,
                OccurredOn = notification.OccurredOn,
                Type = notification.GetType().AssemblyQualifiedName,
                Content = JsonConvert.SerializeObject(notification),
                Processed = false
            });
        }
    }
}
