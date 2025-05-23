using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

namespace SocialMediaBackend.BuildingBlocks.Application;

public abstract class RealtimeSideEffectBase<TMessage> : IRealtimeSideEffect<TMessage>
    where TMessage : IRealtimeMessage
{
    public abstract TMessage Message { get; }
}
