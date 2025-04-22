using Mediator;
using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Application.Abstractions;
public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent;
