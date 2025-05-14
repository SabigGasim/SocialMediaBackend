using Microsoft.AspNetCore.Http;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;

namespace SocialMediaBackend.Modules.Users.Application.Auth;

internal sealed class AuthCommandHandlerWithResultDecorator<TRequest, TResult>
    : AuthRequestHandlerWithResultDecoratorBase<TRequest, TResult, ICommandHandler<TRequest, TResult>>,
    ICommandHandler<TRequest, TResult>
    where TRequest : ICommand<HandlerResponse<TResult>>, IUserRequestBase
{
    public AuthCommandHandlerWithResultDecorator(
        ICommandHandler<TRequest, TResult> decorated,
        IDbConnectionFactory dbConnectionFactory,
        IHttpContextAccessor httpContextAccessor) : base(decorated, dbConnectionFactory, httpContextAccessor)
    {
    }
}
