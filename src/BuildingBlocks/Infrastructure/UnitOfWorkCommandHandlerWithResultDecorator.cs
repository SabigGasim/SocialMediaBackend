using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure;

public class UnitOfWorkCommandHandlerWithResultDecorator<TCommand, TResult>
    : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<HandlerResponse<TResult>>
{
    private readonly ICommandHandler<TCommand, TResult> _decorated;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DbContext _context;

    public UnitOfWorkCommandHandlerWithResultDecorator(
        ICommandHandler<TCommand, TResult> decorated,
        IUnitOfWork unitOfWork,
        DbContext context)
    {
        _decorated = decorated;
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<HandlerResponse<TResult>> ExecuteAsync(TCommand command, CancellationToken ct)
    {
        var handlerResult = await _decorated.ExecuteAsync(command, ct);

        if (command is InternalCommandBase)
        {
            var internalCommand = await _context
                .Set<InternalCommand>()
                .FindAsync([command.Id], ct);

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
