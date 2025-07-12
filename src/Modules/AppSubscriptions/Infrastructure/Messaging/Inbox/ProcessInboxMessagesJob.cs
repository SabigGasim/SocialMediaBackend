using Autofac;
using Quartz;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Messaging.Inbox;

[DisallowConcurrentExecution]
public class ProcessInboxMessagesJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        using (var scope = AppSubscriptionsCompositionRoot.BeginLifetimeScope())
        {
            var handler = scope.Resolve<ICommandHandler<ProcessInboxMessagesCommand>>();
            await handler.ExecuteAsync(new ProcessInboxMessagesCommand(), default);
        }
    }
}
