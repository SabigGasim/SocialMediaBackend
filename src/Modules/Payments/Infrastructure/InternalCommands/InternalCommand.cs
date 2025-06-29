using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.InternalCommands;

public class InternalCommand : AggregateRoot
{
    public InternalCommandBase Command { get; set; } = default!;
    public string? Error { get; set; }
    public string? IdempotencyKey { get; set; }
    public bool Processed { get; set; }
    public DateTimeOffset? ProcessedDate { get; set; }
    public DateTimeOffset EnqueueDate { get; set; }

    private InternalCommand() { }

    public static InternalCommand Create<TCommand>(
        TCommand command,
        bool processed,
        DateTimeOffset enqueueDate,
        DateTimeOffset? processedDate = null,
        string? error = null,
        string? idempotencyKey = null) where TCommand : InternalCommandBase
    {
        return new InternalCommand()
        {
            Id = command.Id,
            Command = command,
            Processed = processed,
            EnqueueDate = enqueueDate,
            ProcessedDate = processedDate,
            Error = error,
            IdempotencyKey = idempotencyKey
        };
    }
}
