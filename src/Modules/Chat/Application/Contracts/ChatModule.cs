using Autofac;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;

namespace SocialMediaBackend.Modules.Chat.Application.Contracts;

internal class ChatModule : IChatModule
{
    public async Task<HandlerResponse<TResult>> ExecuteCommandAsync<TCommand, TResult>(
        TCommand command,
        CancellationToken ct = default)
        where TCommand : ICommand<HandlerResponse<TResult>>
    {
        using (var scope = ChatCompositionRoot.BeginLifetimeScope())
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
        using (var scope = ChatCompositionRoot.BeginLifetimeScope())
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
        using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var handler = scope.Resolve<IQueryHandler<TQuery, TResult>>();

            return await handler.ExecuteAsync(query, ct);
        }
    }
}
