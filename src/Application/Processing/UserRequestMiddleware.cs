using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Processing;

internal class UserRequestMiddleware<TRequest, TResult> : ICommandMiddleware<TRequest, TResult> 
    where TRequest : IUserRequest<TResult>
    where TResult : IHandlerResponse
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ApplicationDbContext _context;

    public UserRequestMiddleware(
        IHttpContextAccessor contextAccessor,
        ApplicationDbContext context)
    {
        _contextAccessor = contextAccessor;
        _context = context;
    }

    public async Task<TResult> ExecuteAsync(TRequest request, CommandDelegate<TResult> next, CancellationToken ct)
    {
        var userIdClaim = _contextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(x => x.Type == "userid")?.Value;

        if(!Guid.TryParse(userIdClaim, out var userId))
        {
            return CreateError("Access token doesn't have a user Id", HandlerResponseStatus.BadRequest, userId);
        }

        var userExists = await _context.Users.AnyAsync(x => x.Id == userId, ct);
        if(!userExists)
        {
            return CreateError("This user doesn't exist", HandlerResponseStatus.Unauthorized, userId);
        }

        request.SetUserId(userId);

        return await next();
    }

    private static TResult CreateError(string messsage, HandlerResponseStatus status, Guid userId)
    {
        return TResult.CreateError<TResult>(messsage, status, userId);
    }
}
