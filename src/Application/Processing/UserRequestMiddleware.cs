using FastEndpoints;
using Microsoft.AspNetCore.Http;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Domain.Services;

namespace SocialMediaBackend.Application.Processing;

internal class UserRequestMiddleware<TRequest, TResult> : ICommandMiddleware<TRequest, TResult>
    where TRequest : IRequest<TResult>, IUserRequest<TRequest>
    where TResult : IHandlerResponse
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserExistsChecker _userExistsChecker;

    public UserRequestMiddleware(
        IHttpContextAccessor contextAccessor,
        IUserExistsChecker userExistsChecker)
    {
        _contextAccessor = contextAccessor;
        _userExistsChecker = userExistsChecker;
    }

    public async Task<TResult> ExecuteAsync(TRequest request, CommandDelegate<TResult> next, CancellationToken ct)
    {
        var userIdClaim = _contextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(x => x.Type == "userid")?.Value;

        if(!Guid.TryParse(userIdClaim, out var userId))
        {
            return CreateError("Access token is expired or doesn't contain a user Id", userId);
        }

        var userExists = await _userExistsChecker.CheckAsync(userId, ct);
        if(!userExists)
        {
            return CreateError("This user doesn't exist", userId);
        }

        var adminClaim = _contextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(x => x.Type == "admin")?.Value;

        var isAdmin = bool.TryParse(adminClaim, out bool isAdminValue) && isAdminValue;

        request.WithUserId(userId)
            .AndAdminRole(isAdmin);

        return await next();
    }

    private static TResult CreateError(string messsage, Guid userId)
    {
        return TResult.CreateError<TResult>(messsage, HandlerResponseStatus.Unauthorized, userId);
    }
}
