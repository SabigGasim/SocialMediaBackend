using Autofac;
using Quartz;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.InternalCommands;

[DisallowConcurrentExecution]
public class ProcessInternalCommandsJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        using (var scope = AppSubscriptionsCompositionRoot.BeginLifetimeScope())
        {
            var handler = scope.Resolve<ICommandHandler<ProcessInternalCommandsCommand>>();
            await handler.ExecuteAsync(new ProcessInternalCommandsCommand(), default);
        }
    }
}
