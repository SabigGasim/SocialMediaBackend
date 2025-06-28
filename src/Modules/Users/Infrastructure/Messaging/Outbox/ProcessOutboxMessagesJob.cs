using Autofac;
using Quartz;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Infrastructure.Configuration;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Messaging.Outbox;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        using (var scope = UsersCompositionRoot.BeginLifetimeScope())
        {
            var handler = scope.Resolve<ICommandHandler<ProcessOutboxMessagesCommand>>();
            await handler.ExecuteAsync(new ProcessOutboxMessagesCommand(), default);
        }
    }
}