using Quartz;
using SocialMediaBackend.Modules.Chat.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Chat.Infrastructure.Messaging.Inbox;
using SocialMediaBackend.Modules.Chat.Infrastructure.Messaging.Outbox;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.Quartz;

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
            .WithIdentity($"Chat.{name}")
            .Build();

        var schedulerFactory = SchedulerBuilder.Create()
            .UseInMemoryStore()
            .UseDefaultThreadPool(1)
            .WithName($"Chat.{name}Scheduler")
            .Build();

        var scheduler = await schedulerFactory.GetScheduler();

        await scheduler.ScheduleJob(job, trigger);

        await scheduler.Start();
    }
}
