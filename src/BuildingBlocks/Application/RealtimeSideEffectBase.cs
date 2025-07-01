using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

namespace SocialMediaBackend.BuildingBlocks.Application;

public abstract class RealtimeSideEffectBase<TMessage, THub> : IRealtimeSideEffect<TMessage>
    where TMessage : IRealtimeMessage
    where THub : Hub
{
    public abstract TMessage Message { get; }
    public abstract IHubContext<THub> HubContext { get; }
}
