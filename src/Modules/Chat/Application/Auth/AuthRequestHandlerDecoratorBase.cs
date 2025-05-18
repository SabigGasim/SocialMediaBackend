using Microsoft.AspNetCore.Http;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Application.Auth;

internal abstract class AuthRequestHandlerDecoratorBase<TRequest, TRequestHandler> : IRequestHandler<TRequest>
    where TRequest : IRequest<HandlerResponse>, IUserRequestBase
    where TRequestHandler : IRequestHandler<TRequest>
{
    private readonly TRequestHandler _decorated;
    private readonly IChatterRepository _chatterRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthRequestHandlerDecoratorBase(
        TRequestHandler decorated,
        IChatterRepository chatterRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _decorated = decorated;
        _chatterRepository = chatterRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<HandlerResponse> ExecuteAsync(TRequest request, CancellationToken ct)
    {
        var userIdClaim = _httpContextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(x => x.Type == "userid")?.Value;

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return ("Access token is expired or doesn't contain a user Id", HandlerResponseStatus.Unauthorized, userId);
        }

        if (!await _chatterRepository.ExistsAsync(new ChatterId(userId), ct))
        {
            return ("This user doesn't exist", HandlerResponseStatus.Unauthorized, userId);
        }

        var adminClaim = _httpContextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(x => x.Type == "admin")?.Value;

        var isAdmin = bool.TryParse(adminClaim, out bool isAdminValue) && isAdminValue;

        request.WithUserId(userId);
        request.WithAdminRole(isAdmin);

        return await _decorated.ExecuteAsync(request, ct);
    }
}
