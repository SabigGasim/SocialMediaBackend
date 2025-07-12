using Quartz;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Messaging.Inbox;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Messaging.Outbox;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration.Quartz;

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
        var name = typeof(TJob).Name;

        var trigger = TriggerBuilder.Create()
           .StartNow()
           .WithSimpleSchedule(x => x
               .WithIntervalInSeconds(2)
               .RepeatForever())
           .Build();

        var job = JobBuilder.Create<TJob>()
            .WithIdentity($"AppSubscriptions.{name}")
            .Build();

        var schedulerFactory = SchedulerBuilder.Create()
            .UseInMemoryStore()
            .UseDefaultThreadPool(1)
            .WithName($"AppSubscriptions.{name}Scheduler")
            .Build();

        var scheduler = await schedulerFactory.GetScheduler();

        await scheduler.ScheduleJob(job, trigger);

        await scheduler.Start();
    }
}
