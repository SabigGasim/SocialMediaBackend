using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

public abstract class InternalCommandBase(Guid id) : ICommand<HandlerResponse>
{
    public Guid Id { get; } = id != default ? id : Guid.CreateVersion7();
}
