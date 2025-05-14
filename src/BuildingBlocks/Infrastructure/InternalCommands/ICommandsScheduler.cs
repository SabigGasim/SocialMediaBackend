namespace SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

public interface ICommandsScheduler
{
    ValueTask EnqueueAsync<TInternalCommand>(TInternalCommand command)
        where TInternalCommand : InternalCommandBase;
}
