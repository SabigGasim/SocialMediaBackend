using Marten;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Payments.Infrastructure.InternalCommands;
using BB = SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Processing;

public sealed class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand<HandlerResponse>
{
    private readonly ICommandHandler<TCommand> _decorated;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDocumentSession _session;

    public UnitOfWorkCommandHandlerDecorator(
        ICommandHandler<TCommand> decorated,
        IUnitOfWork unitOfWork,
        IDocumentSession session)
    {
        _decorated = decorated;
        _unitOfWork = unitOfWork;
        _session = session;
    }

    public async Task<HandlerResponse> ExecuteAsync(TCommand command, CancellationToken ct)
    {
        var handlerResult = await _decorated.ExecuteAsync(command, ct);

        if (command is BB.InternalCommandBase)
        {
            var internalCommand = await _session.LoadAsync<InternalCommand>(command.Id, ct);

            if (internalCommand is not null)
            {
                internalCommand.Processed = true;
                internalCommand.ProcessedDate = TimeProvider.System.GetUtcNow();

                _session.Store(internalCommand);
            }
        }

        await _unitOfWork.CommitAsync(ct);
        
        return handlerResult;
    }
}
