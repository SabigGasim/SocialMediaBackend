using Autofac;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Application.Helpers;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;

namespace SocialMediaBackend.Modules.Chat.Application.Contracts;

public class ChatModule : IChatModule
{
    public async Task<HandlerResponse<TResult>> ExecuteCommandAsync<TCommand, TResult>(
        TCommand command,
        CancellationToken ct = default)
        where TCommand : ICommand<HandlerResponse<TResult>>
    {
        return await CommandExecutor.ExecuteAsync<TCommand, TResult>(command, ct);
    }

    public async Task<HandlerResponse> ExecuteCommandAsync<TCommand>(
        TCommand command,
        CancellationToken ct = default)
        where TCommand : ICommand<HandlerResponse>
    {
        return await CommandExecutor.ExecuteAsync(command, ct);
    }

    public async Task<HandlerResponse<TResult>> ExecuteQueryAsync<TQuery, TResult>(
        TQuery query,
        CancellationToken ct = default)
        where TQuery : IQuery<HandlerResponse<TResult>>
    {
        using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var handler = scope.Resolve<IQueryHandler<TQuery, TResult>>();

            return await handler.ExecuteAsync(query, ct);
        }
    }

    public async Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IEvent
    {
        using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var mediator = scope.Resolve<Mediator.IMediator>();

            await mediator.Publish(@event, cancellationToken);
        }
    }

    public async Task Publish(object @event, CancellationToken cancellationToken = default)
    {
        using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var mediator = scope.Resolve<Mediator.IMediator>();

            await mediator.Publish(@event, cancellationToken);
        }
    }
}
