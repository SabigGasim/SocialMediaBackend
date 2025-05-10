using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

public abstract class InternalCommandBase : ICommand<HandlerResponse>
{
    protected InternalCommandBase(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
