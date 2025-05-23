using Mediator;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

namespace SocialMediaBackend.BuildingBlocks.Application;

public interface IRealtimeSideEffect : INotification;

public interface IRealtimeSideEffect<TMessage> : IRealtimeSideEffect
    where TMessage : IRealtimeMessage;
