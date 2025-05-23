using Mediator;

namespace SocialMediaBackend.BuildingBlocks.Application;

public interface IRealtimeSideEffectHandler<TSideEffect> : INotificationHandler<TSideEffect>
    where TSideEffect : IRealtimeSideEffect;
