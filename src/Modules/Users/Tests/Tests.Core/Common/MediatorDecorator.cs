using Mediator;
using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Users.Tests.Core.Common;

public class MediatorDecorator(IMediator mediator) : IMediator
{
    private readonly IMediator _mediator = mediator;
    public List<IDomainEvent> PublishedEvents { get; } = new();

    public ValueTask Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
    {
        if (notification is IDomainEvent domainEvent)
            PublishedEvents.Add(domainEvent);

        return _mediator.Publish(notification, cancellationToken);
    }

    public ValueTask Publish(object notification, CancellationToken cancellationToken = default)
    {
        if (notification is IDomainEvent domainEvent)
            PublishedEvents.Add(domainEvent);

        return _mediator.Publish(notification, cancellationToken);
    }

    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        return _mediator.CreateStream(query, cancellationToken);
    }

    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        return _mediator.CreateStream(request, cancellationToken);
    }

    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamCommand<TResponse> command, CancellationToken cancellationToken = default)
    {
        return _mediator.CreateStream(command, cancellationToken);
    }

    public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
    {
        return _mediator.CreateStream(request, cancellationToken);
    }

    public ValueTask<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(request, cancellationToken);
    }

    public ValueTask<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(command, cancellationToken);
    }

    public ValueTask<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(query, cancellationToken);
    }

    public ValueTask<object?> Send(object message, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(message, cancellationToken);
    }
}
