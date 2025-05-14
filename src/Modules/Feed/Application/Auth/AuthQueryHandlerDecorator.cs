using Microsoft.AspNetCore.Http;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Infrastructure;

namespace SocialMediaBackend.Modules.Feed.Application.Auth;

internal sealed class AuthQueryHandlerDecorator<TRequest, TResult>
    : AuthRequestHandlerWithResultDecoratorBase<TRequest, TResult, IQueryHandler<TRequest, TResult>>,
    IQueryHandler<TRequest, TResult>
    where TRequest : IQuery<HandlerResponse<TResult>>, IUserRequestBase
{
    public AuthQueryHandlerDecorator(
        IQueryHandler<TRequest, TResult> decorated,
        IDbConnectionFactory dbConnectionFactory,
        IHttpContextAccessor httpContextAccessor) : base(decorated, dbConnectionFactory, httpContextAccessor)
    {
    }
}
