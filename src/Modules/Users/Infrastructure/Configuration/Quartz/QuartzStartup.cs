using Quartz;
using SocialMediaBackend.Modules.Users.Infrastructure.Messaging.Outbox;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Configuration.Quartz;

public static class QuartzStartup
{
    public static async Task InitializeAsync()
    {
        var trigger = TriggerBuilder.Create()
           .StartNow()
           .WithSimpleSchedule(x => x
               .WithIntervalInSeconds(2)
               .RepeatForever())
           .Build();

        var job = JobBuilder.Create<ProcessOutboxMessagesJob>()
            .WithIdentity("Users.ProcessOutboxMessagesJob")
            .Build();

        var schedulerFactory = SchedulerBuilder.Create()
            .UseInMemoryStore()
            .UseDefaultThreadPool(1)
            .WithName("Users.ProcessOutboxMessagesJob.Scheduler")
            .Build();

        var scheduler = await schedulerFactory.GetScheduler();

        await scheduler.ScheduleJob(job, trigger);

        await scheduler.Start();
    }
}
