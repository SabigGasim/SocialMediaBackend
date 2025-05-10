using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

public interface ICommandsScheduler
{
    ValueTask EnqueueAsync<T>(ICommand<T> command);
}
