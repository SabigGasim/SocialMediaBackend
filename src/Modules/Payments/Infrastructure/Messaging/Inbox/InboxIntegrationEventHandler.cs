using Dapper;
using Newtonsoft.Json;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Messaging.Inbox;

public class InboxIntegrationEventHandler<TEvent>(IDbConnectionFactory factory) 
    : IIntegrationEventHandler<TEvent> where TEvent : IntegrationEvent
{
    private readonly IDbConnectionFactory _factory = factory;

    public async ValueTask Handle(TEvent notification, CancellationToken cancellationToken)
    {
        const string sqlInsert =
                $"""
                INSERT INTO {Schema.Payments}."InboxMessages" ("Id", "Type" , "Content", "OccurredOn", "Processed")
                VALUES (@Id, @Type, @Content, @OccurredOn, @Processed)
                """;

        using (var connection = await _factory.CreateAsync(CancellationToken.None))
        {
            await connection.ExecuteAsync(sqlInsert, new
            {
                notification.Id,
                notification.OccurredOn,
                Type = notification.GetType().AssemblyQualifiedName,
                Content = JsonConvert.SerializeObject(notification),
                Processed = false
            });
        }

    }
}
