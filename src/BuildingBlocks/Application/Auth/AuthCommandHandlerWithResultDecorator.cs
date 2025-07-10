using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;

namespace SocialMediaBackend.BuildingBlocks.Application.Auth;

public sealed class AuthCommandHandlerWithResultDecorator<TRequest, TResult>
    : AuthRequestHandlerWithResultDecoratorBase<TRequest, TResult, ICommandHandler<TRequest, TResult>>,
    ICommandHandler<TRequest, TResult>
    where TRequest : ICommand<HandlerResponse<TResult>>, IUserRequestBase, IRequireAuthorization
{
    public AuthCommandHandlerWithResultDecorator(
        ICommandHandler<TRequest, TResult> decorated,
        IPermissionManager permissionManager,
        IExecutionContextAccessor executionContextAccessor) : base(decorated, permissionManager, executionContextAccessor)
    {
    }
}
