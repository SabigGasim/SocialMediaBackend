using Microsoft.AspNetCore.Http;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Application.Auth;

internal sealed class AuthCommandHandlerDecorator<TRequest>
    : AuthRequestHandlerDecoratorBase<TRequest, ICommandHandler<TRequest>>, ICommandHandler<TRequest>
    where TRequest : ICommand<HandlerResponse>, IUserRequestBase
{
    public AuthCommandHandlerDecorator(
        ICommandHandler<TRequest> decorated,
        IChatterRepository chatterRepository,
        IHttpContextAccessor httpContextAccessor) : base(decorated, chatterRepository, httpContextAccessor)
    {
    }
}
