using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;

namespace SocialMediaBackend.BuildingBlocks.Application.Auth;

public sealed class AuthCommandHandlerDecorator<TRequest>
    : AuthRequestHandlerDecoratorBase<TRequest, ICommandHandler<TRequest>>, ICommandHandler<TRequest>
    where TRequest : ICommand<HandlerResponse>, IRequireAuthorization
{
    public AuthCommandHandlerDecorator(
        ICommandHandler<TRequest> decorated,
        IPermissionManager permissionManager,
        IExecutionContextAccessor executionContextAccessor) : base(decorated, permissionManager, executionContextAccessor)
    {
    }
}
