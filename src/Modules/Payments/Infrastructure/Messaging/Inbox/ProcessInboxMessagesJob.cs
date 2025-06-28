using Quartz;
using SocialMediaBackend.Modules.Payments.Infrastructure.Helpers;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Messaging.Inbox;

[DisallowConcurrentExecution]
public class ProcessInboxMessagesJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await CommandExecutor.ExecuteAsync(new ProcessInboxMessagesCommand());
    }
}
