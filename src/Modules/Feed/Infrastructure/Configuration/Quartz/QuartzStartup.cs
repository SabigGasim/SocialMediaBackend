using Quartz;
using SocialMediaBackend.Modules.Feed.Infrastructure.InternalCommands;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Configuration.Quartz;

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

        var job = JobBuilder.Create<ProcessInternalCommandsJob>()
            .WithIdentity("Feed.ProcessInternalCommandsJob")
            .Build();

        var schedulerFactory = SchedulerBuilder.Create()
            .UseInMemoryStore()
            .UseDefaultThreadPool(1)
            .Build();

        var scheduler = await schedulerFactory.GetScheduler();

        await scheduler.ScheduleJob(job, trigger);

        await scheduler.Start();
    }
}
