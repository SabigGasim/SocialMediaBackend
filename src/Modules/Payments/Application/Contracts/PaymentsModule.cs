﻿using Autofac;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration;

namespace SocialMediaBackend.Modules.Payments.Application.Contracts;

public class PaymentsModule : IPaymentsModule
{
    public async Task<HandlerResponse<TResult>> ExecuteCommandAsync<TCommand, TResult>(
        TCommand command,
        CancellationToken ct = default)
        where TCommand : ICommand<HandlerResponse<TResult>>
    {
        using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
        {
            var handler = scope.Resolve<ICommandHandler<TCommand, TResult>>();

            return await handler.ExecuteAsync(command, ct);
        }
    }

    public async Task<HandlerResponse> ExecuteCommandAsync<TCommand>(
        TCommand command,
        CancellationToken ct = default)
        where TCommand : ICommand<HandlerResponse>
    {
        using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
        {
            var handler = scope.Resolve<ICommandHandler<TCommand>>();

            return await handler.ExecuteAsync(command, ct);
        }
    }

    public async Task<HandlerResponse<TResult>> ExecuteQueryAsync<TQuery, TResult>(
        TQuery query,
        CancellationToken ct = default)
        where TQuery : IQuery<HandlerResponse<TResult>>
    {
        using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
        {
            var handler = scope.Resolve<IQueryHandler<TQuery, TResult>>();

            return await handler.ExecuteAsync(query, ct);
        }
    }

    public async Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IEvent
    {
        using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
        {
            var mediator = scope.Resolve<Mediator.IMediator>();

            await mediator.Publish(@event, cancellationToken);
        }
    }

    public async Task Publish(object @event, CancellationToken cancellationToken = default)
    {
        using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
        {
            var mediator = scope.Resolve<Mediator.IMediator>();

            await mediator.Publish(@event, cancellationToken);
        }
    }
}
