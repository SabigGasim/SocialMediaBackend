using Microsoft.AspNetCore.Http;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Application.Auth;

internal sealed class AuthQueryHandlerDecorator<TRequest, TResult>
    : AuthRequestHandlerWithResultDecoratorBase<TRequest, TResult, IQueryHandler<TRequest, TResult>>,
    IQueryHandler<TRequest, TResult>
    where TRequest : IQuery<HandlerResponse<TResult>>, IUserRequestBase
{
    public AuthQueryHandlerDecorator(
        IQueryHandler<TRequest, TResult> decorated,
        IChatterRepository chatterRepository,
        IHttpContextAccessor httpContextAccessor) : base(decorated, chatterRepository, httpContextAccessor)
    {
    }
}
