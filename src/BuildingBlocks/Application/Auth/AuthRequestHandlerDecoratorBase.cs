using SocialMediaBackend.BuildingBlocks.Application.Requests;
using System.Reflection;

namespace SocialMediaBackend.BuildingBlocks.Application.Auth;

public abstract class AuthRequestHandlerDecoratorBase<TRequest, TRequestHandler> : IRequestHandler<TRequest>
    where TRequest : IRequest<HandlerResponse>, IRequireAuthorization
    where TRequestHandler : IRequestHandler<TRequest>
{
    private readonly TRequestHandler _decorated;
    private readonly IExecutionContextAccessor _executionContext;
    private readonly IPermissionManager _permissionManager;
    private static readonly int[] _requiredPermissions = LoadPermissions();

    public AuthRequestHandlerDecoratorBase(
        TRequestHandler decorated,
        IPermissionManager permissionManager,
        IExecutionContextAccessor userContext)
    {
        _decorated = decorated;
        _permissionManager = permissionManager;
        _executionContext = userContext;
    }

    public async Task<HandlerResponse> ExecuteAsync(TRequest request, CancellationToken ct)
    {
        if (_requiredPermissions.Length <= 0 || AuthorizationIsOptionalAndUserIsntAuthenticated(request))
        {
            return await _decorated.ExecuteAsync(request, ct);
        }

        var userId = _executionContext.UserId;

        var authorized = _requiredPermissions.Length == 1
            ? await _permissionManager.UserHasPermission(userId, _requiredPermissions[0], ct)
            : await _permissionManager.UserHasPermissions(userId, _requiredPermissions, ct);

        if (!authorized)
        {
            return ("User doesn't exist or doesn't have sufficient access", HandlerResponseStatus.Unauthorized);
        }

        return await _decorated.ExecuteAsync(request, ct);
    }

    private bool AuthorizationIsOptionalAndUserIsntAuthenticated(TRequest request)
    {
        return request is IRequireOptionalAuthorizaiton && !_executionContext.IsAvailable;
    }

    private static int[] LoadPermissions()
    {
        return typeof(TRequest)
            .GetCustomAttributes<HasPermissionAttribute>(true)
            .SelectMany(x => x.Permissions)
            .ToArray();
    }
}
