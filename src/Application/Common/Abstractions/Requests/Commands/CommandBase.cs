
namespace SocialMediaBackend.Application.Common.Abstractions.Requests.Commands;

public abstract class CommandBase : ICommand
{
    public Guid Id { get; }

    protected CommandBase(Guid id) => Id = id;

    protected CommandBase() => Id = Guid.NewGuid();
}

public abstract class CommandBase<TResponse> : ICommand<HandlerResponse<TResponse>>
{
    public Guid Id { get; }

    protected CommandBase(Guid id) => Id = id;

    protected CommandBase() => Id = Guid.NewGuid();
}
