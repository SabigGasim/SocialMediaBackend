using Autofac;
using Marten;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.InternalCommands;

public class CommandsScheduler : ICommandsScheduler
{
    public async ValueTask EnqueueAsync<TInternalCommand>(TInternalCommand command, string? idempotencyKey = null)
        where TInternalCommand : InternalCommandBase
    {
        var internalCommand = InternalCommand.Create(
            command,
            processed: false,
            enqueueDate: DateTimeOffset.UtcNow,
            idempotencyKey: idempotencyKey);

        using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
        {
            var session = scope.Resolve<IDocumentSession>();

            session.Store(internalCommand);

            await session.SaveChangesAsync();
        }
    }
}

