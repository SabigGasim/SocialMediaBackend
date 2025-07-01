using Autofac;
using SocialMediaBackend.BuildingBlocks.Infrastructure;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.SIgnalR;

public class SignalRModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<InMemoryHubConnectionTracker>()
            .As<IHubConnectionTracker>()
            .SingleInstance();
    }
}
