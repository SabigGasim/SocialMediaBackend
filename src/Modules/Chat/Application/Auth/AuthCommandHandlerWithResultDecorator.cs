using Microsoft.AspNetCore.Http;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Application.Auth;

internal sealed class AuthCommandHandlerWithResultDecorator<TRequest, TResult>
    : AuthRequestHandlerWithResultDecoratorBase<TRequest, TResult, ICommandHandler<TRequest, TResult>>,
    ICommandHandler<TRequest, TResult>
    where TRequest : ICommand<HandlerResponse<TResult>>, IUserRequestBase
{
    public AuthCommandHandlerWithResultDecorator(
        ICommandHandler<TRequest, TResult> decorated,
        IChatterRepository chatterRepository,
        IHttpContextAccessor httpContextAccessor) : base(decorated, chatterRepository, httpContextAccessor)
    {
    }
}
