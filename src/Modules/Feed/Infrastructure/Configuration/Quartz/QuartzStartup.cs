using Quartz;
using SocialMediaBackend.Modules.Feed.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Feed.Infrastructure.Messaging.Inbox;
using SocialMediaBackend.Modules.Feed.Infrastructure.Messaging.Outbox;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Configuration.Quartz;

public static class QuartzStartup
{
    public static async Task InitializeAsync()
    {
        await Task.WhenAll(
            SchduleJobAsync<ProcessInternalCommandsJob>(),
            SchduleJobAsync<ProcessInboxMessagesJob>(),
            SchduleJobAsync<ProcessOutboxMessagesJob>()
            );
    }

    private static async Task SchduleJobAsync<TJob>()
        where TJob : IJob
    {
        var name = typeof(TJob).Name;

        var trigger = TriggerBuilder.Create()
           .StartNow()
           .WithSimpleSchedule(x => x
               .WithIntervalInSeconds(4)
               .RepeatForever())
           .Build();

        var job = JobBuilder.Create<TJob>()
            .WithIdentity($"Feed.{name}")
            .Build();

        var schedulerFactory = SchedulerBuilder.Create()
            .UseInMemoryStore()
            .UseDefaultThreadPool(1)
            .WithName($"Feed.{name}Scheduler")
            .Build();

        var scheduler = await schedulerFactory.GetScheduler();

        await scheduler.ScheduleJob(job, trigger);

        await scheduler.Start();
    }
}
