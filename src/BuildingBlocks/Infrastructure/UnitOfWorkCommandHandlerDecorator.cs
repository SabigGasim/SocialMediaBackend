using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure;

public sealed class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand<HandlerResponse>
{
    private readonly ICommandHandler<TCommand> _decorated;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DbContext _context;

    public UnitOfWorkCommandHandlerDecorator(
        ICommandHandler<TCommand> decorated,
        IUnitOfWork unitOfWork,
        DbContext context)
    {
        _decorated = decorated;
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<HandlerResponse> ExecuteAsync(TCommand command, CancellationToken ct)
    {
        var handlerResult = await _decorated.ExecuteAsync(command, ct);
        if (!handlerResult.IsSuccess)
        {
            return handlerResult;
        }

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
