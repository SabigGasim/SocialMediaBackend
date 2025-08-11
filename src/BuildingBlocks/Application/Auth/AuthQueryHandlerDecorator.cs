using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;

namespace SocialMediaBackend.BuildingBlocks.Application.Auth;

public sealed class AuthQueryHandlerDecorator<TRequest, TResult>
    : AuthRequestHandlerWithResultDecoratorBase<TRequest, TResult, IQueryHandler<TRequest, TResult>>,
    IQueryHandler<TRequest, TResult>
    where TRequest : IQuery<HandlerResponse<TResult>>, IRequireAuthorization
{
    public AuthQueryHandlerDecorator(
        IQueryHandler<TRequest, TResult> decorated,
        IPermissionManager permissionManager,
        IExecutionContextAccessor executionContextAccessor) : base(decorated, permissionManager, executionContextAccessor)
    {
    }
}
