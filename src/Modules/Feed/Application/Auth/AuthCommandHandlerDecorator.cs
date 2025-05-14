using Microsoft.AspNetCore.Http;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;

namespace SocialMediaBackend.Modules.Feed.Application.Auth;
internal sealed class AuthCommandHandlerDecorator<TRequest>
    : AuthRequestHandlerDecoratorBase<TRequest, ICommandHandler<TRequest>>, ICommandHandler<TRequest>
    where TRequest : ICommand<HandlerResponse>, IUserRequestBase
{
    public AuthCommandHandlerDecorator(
        ICommandHandler<TRequest> decorated,
        IDbConnectionFactory dbConnectionFactory,
        IHttpContextAccessor httpContextAccessor) : base(decorated, dbConnectionFactory, httpContextAccessor)
    {
    }
}
