using Marten;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Processing;

public class UnitOfWorkCommandHandlerWithResultDecorator<TCommand, TResult>
    : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<HandlerResponse<TResult>>
{
    private readonly ICommandHandler<TCommand, TResult> _decorated;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDocumentSession _session;

    public UnitOfWorkCommandHandlerWithResultDecorator(
        ICommandHandler<TCommand, TResult> decorated,
        IUnitOfWork unitOfWork,
        IDocumentSession session)
    {
        _decorated = decorated;
        _unitOfWork = unitOfWork;
        _session = session;
    }

    public async Task<HandlerResponse<TResult>> ExecuteAsync(TCommand command, CancellationToken ct)
    {
        var handlerResult = await _decorated.ExecuteAsync(command, ct);
        if (!handlerResult.IsSuccess)
        {
            return handlerResult;
        }

        if (command is InternalCommandBase)
        {
            var internalCommand = await _session.LoadAsync<InternalCommand>(command.Id, ct);

            if (internalCommand != null)
            {
                internalCommand.Processed = true;
                internalCommand.ProcessedDate = TimeProvider.System.GetUtcNow();
            }
        }

        await _unitOfWork.CommitAsync(ct);

        return handlerResult;
    }
}
