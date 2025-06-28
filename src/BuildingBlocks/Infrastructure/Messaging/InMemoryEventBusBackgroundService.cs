using Microsoft.Extensions.Hosting;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;

internal class InMemoryEventBusBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await InMemoryEventBus.Instance.PublishQueuedEvents(stoppingToken);
        }
    }
}
