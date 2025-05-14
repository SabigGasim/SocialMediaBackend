using Dapper;
using Microsoft.AspNetCore.Http;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Auth;

internal class AuthRequestHandlerWithResultDecoratorBase<TRequest, TResult, TRequestHandler>
    : IRequestHandler<TRequest, TResult>
    where TRequest : IRequest<HandlerResponse<TResult>>, IUserRequestBase
    where TRequestHandler : IRequestHandler<TRequest, TResult>
{
    private readonly TRequestHandler _decorated;
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthRequestHandlerWithResultDecoratorBase(
        TRequestHandler decorated,
        IDbConnectionFactory dbConnectionFactory,
        IHttpContextAccessor httpContextAccessor)
    {
        _decorated = decorated;
        _dbConnectionFactory = dbConnectionFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<HandlerResponse<TResult>> ExecuteAsync(TRequest request, CancellationToken ct)
    {
        var userIdClaim = _httpContextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(x => x.Type == "userid")?.Value;

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return ("Access token is expired or doesn't contain a user Id", HandlerResponseStatus.Unauthorized, userId);
        }

        using (var connection = await _dbConnectionFactory.CreateAsync(ct))
        {
            const string sql = $"""
                SELECT EXISTS (
                    SELECT 1
                    FROM {Schema.Feed}."Authors"
                    WHERE "Id" = @Id
                );
                """;

            var userExists = await connection.ExecuteScalarAsync<bool>(
                new CommandDefinition(sql, new { Id = userId }, cancellationToken: ct));

            if (!userExists)
            {
                return ("This user doesn't exist", HandlerResponseStatus.Unauthorized, userId);
            }
        }

        var adminClaim = _httpContextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(x => x.Type == "admin")?.Value;

        var isAdmin = bool.TryParse(adminClaim, out bool isAdminValue) && isAdminValue;

        request.WithUserId(userId);
        request.WithAdminRole(isAdmin);

        return await _decorated.ExecuteAsync(request, ct);
    }
}