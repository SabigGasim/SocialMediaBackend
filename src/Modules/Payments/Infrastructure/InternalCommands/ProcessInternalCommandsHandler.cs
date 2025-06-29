using Autofac;
using Dapper;
using Polly;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.InternalCommands;

public class ProcessInternalCommandsCommandHandler(IAggregateRepository repository) 
    : ICommandHandler<ProcessInternalCommandsCommand>
{
    private readonly IAggregateRepository _repository = repository;

    public async Task<HandlerResponse> ExecuteAsync(ProcessInternalCommandsCommand command, CancellationToken ct)
    {
        var commands = await _repository.LoadManyAsync<InternalCommand>(
            x => x.Processed == false
            && x.Error == null,
            ct);

        var internalCommandsList = commands.AsList();
        
        var policy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
            [
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(3)
            ]);

        foreach (var internalCommand in internalCommandsList)
        {
            var result = await policy.ExecuteAndCaptureAsync(() => ProcessCommand((dynamic)internalCommand.Command));

            if (result.Outcome == OutcomeType.Successful)
            {
                continue;
            }

            internalCommand.Processed = false;
            internalCommand.Error = result.FinalException.Message;

            _repository.Store(internalCommand);
            await _repository.SaveChangesAsync(CancellationToken.None);
        }

        return HandlerResponseStatus.NoContent;
    }

    private static async Task ProcessCommand<TInternalCommand>(TInternalCommand internalCommand)
        where TInternalCommand : InternalCommandBase
    {
        await using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
        {
            var handler = scope.Resolve<ICommandHandler<TInternalCommand>>();

            await handler.ExecuteAsync(internalCommand, CancellationToken.None);
        }
    }
}
