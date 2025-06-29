using Quartz;
using SocialMediaBackend.Modules.Payments.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Infrastructure.Messaging.Inbox;
using SocialMediaBackend.Modules.Payments.Infrastructure.Messaging.Outbox;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Configuration.Quartz;

public static class QuartzStartup
{
    public static async Task InitializeAsync()
    {
        await Task.WhenAll(
            SchduleJobAsync<ProcessInternalCommandsJob>(),
            SchduleJobAsync<ProcessOutboxMessagesJob>(),
            SchduleJobAsync<ProcessInboxMessagesJob>()
            );
    }

    private static async Task SchduleJobAsync<TJob>()
        where TJob : IJob
    {
        var jobName = typeof(TJob).Name;

        var trigger = TriggerBuilder.Create()
           .StartNow()
           .WithSimpleSchedule(x => x
               .WithIntervalInSeconds(2)
               .RepeatForever())
           .Build();

        var job = JobBuilder.Create<TJob>()
            .WithIdentity($"Payments.{jobName}")
            .Build();

        var schedulerFactory = SchedulerBuilder.Create()
            .UseInMemoryStore()
            .UseDefaultThreadPool(1)
            .WithName($"Payments.{jobName}Scheduler")
            .Build();

        var scheduler = await schedulerFactory.GetScheduler();

        await scheduler.ScheduleJob(job, trigger);

        await scheduler.Start();
    }
}
