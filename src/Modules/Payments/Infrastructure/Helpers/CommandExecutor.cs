using Autofac;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Helpers;

public static class CommandExecutor
{
    public static async Task<HandlerResponse> ExecuteAsync<TCommand>(TCommand command, CancellationToken ct = default)
        where TCommand : ICommand<HandlerResponse>
    {
        await using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
        {
            var handler = scope.Resolve<ICommandHandler<TCommand>>();

            return await handler.ExecuteAsync(command, ct);
        }
    }

    public static async Task<HandlerResponse<TResult>> ExecuteAsync<TCommand, TResult>(TCommand command, CancellationToken ct = default)
        where TCommand : ICommand<HandlerResponse<TResult>>
    {
        await using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
        {
            var handler = scope.Resolve<ICommandHandler<TCommand, TResult>>();

            return await handler.ExecuteAsync(command, ct);
        }
    }
}
