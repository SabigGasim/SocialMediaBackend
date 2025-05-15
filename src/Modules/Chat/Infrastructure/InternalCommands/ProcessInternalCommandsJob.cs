using Autofac;
using Quartz;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.InternalCommands;

[DisallowConcurrentExecution]
public class ProcessInternalCommandsJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var handler = scope.Resolve<ICommandHandler<ProcessInternalCommandsCommand>>();
            await handler.ExecuteAsync(new ProcessInternalCommandsCommand(), default);
        }
    }
}
