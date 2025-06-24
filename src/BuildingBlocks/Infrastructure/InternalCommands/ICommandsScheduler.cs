namespace SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

public interface ICommandsScheduler
{
    ValueTask EnqueueAsync<TInternalCommand>(TInternalCommand command, string? idempotencyKey = null)
        where TInternalCommand : InternalCommandBase;
}
