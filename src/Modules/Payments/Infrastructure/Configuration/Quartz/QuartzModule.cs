using Autofac;
using Quartz;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Configuration.Quartz;

public class QuartzModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //builder
        //    .RegisterAssemblyTypes(typeof(IInfrastructureMarker).Assembly)
        //    .Where(x => typeof(IJob).IsAssignableFrom(x))
        //    .InstancePerDependency();
    }
}
