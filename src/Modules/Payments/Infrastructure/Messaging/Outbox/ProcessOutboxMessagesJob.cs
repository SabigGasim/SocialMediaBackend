using Autofac;
using Quartz;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Messaging.Outbox;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
        {
            var handler = scope.Resolve<ICommandHandler<ProcessOutboxMessagesCommand>>();
            await handler.ExecuteAsync(new ProcessOutboxMessagesCommand(), default);
        }
    }
}