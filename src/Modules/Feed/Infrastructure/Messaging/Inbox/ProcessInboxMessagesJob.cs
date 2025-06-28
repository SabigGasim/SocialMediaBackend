using Autofac;
using Quartz;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Messaging.Inbox;

[DisallowConcurrentExecution]
public class ProcessInboxMessagesJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        using (var scope = FeedCompositionRoot.BeginLifetimeScope())
        {
            var handler = scope.Resolve<ICommandHandler<ProcessInboxMessagesCommand>>();
            await handler.ExecuteAsync(new ProcessInboxMessagesCommand(), default);
        }
    }
}
