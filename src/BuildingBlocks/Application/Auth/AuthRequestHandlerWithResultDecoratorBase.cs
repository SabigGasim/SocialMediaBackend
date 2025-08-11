using SocialMediaBackend.BuildingBlocks.Application.Requests;
using System.Reflection;

namespace SocialMediaBackend.BuildingBlocks.Application.Auth;

public class AuthRequestHandlerWithResultDecoratorBase<TRequest, TResult, TRequestHandler>
    : IRequestHandler<TRequest, TResult>
    where TRequest : IRequest<HandlerResponse<TResult>>, IRequireAuthorization
    where TRequestHandler : IRequestHandler<TRequest, TResult>
{
    private readonly TRequestHandler _decorated;
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly IPermissionManager _permissionManager;
    private static readonly int[] _requiredPermissions = LoadPermissions();

    public AuthRequestHandlerWithResultDecoratorBase(
        TRequestHandler decorated,
        IPermissionManager permissionManager,
        IExecutionContextAccessor executionContextAccessor)
    {
        _decorated = decorated;
        _permissionManager = permissionManager;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<HandlerResponse<TResult>> ExecuteAsync(TRequest request, CancellationToken ct)
    {
        var userId = _executionContextAccessor.UserId;

        if (_requiredPermissions.Length <= 0)
        {
            return await _decorated.ExecuteAsync(request, ct);
        }

        var authorized = _requiredPermissions.Length == 1
            ? await _permissionManager.UserHasPermission(userId, _requiredPermissions[0], ct)
            : await _permissionManager.UserHasPermissions(userId, _requiredPermissions, ct);

        if (!authorized)
        {
            return ("User doesn't exist or doesn't have sufficient access", HandlerResponseStatus.Unauthorized);
        }

        return await _decorated.ExecuteAsync(request, ct);
    }

    private static int[] LoadPermissions()
    {
        return typeof(TRequest)
            .GetCustomAttributes<HasPermissionAttribute>(true)
            .SelectMany(x => x.Permissions)
            .ToArray();
    }
}