using Autofac;
using Quartz;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Messaging.Outbox;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        using (var scope = FeedCompositionRoot.BeginLifetimeScope())
        {
            var handler = scope.Resolve<ICommandHandler<ProcessOutboxMessagesCommand>>();
            await handler.ExecuteAsync(new ProcessOutboxMessagesCommand(), default);
        }
    }
}