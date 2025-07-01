using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Contracts;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.BuildingBlocks.Domain.Exceptions;

namespace SocialMediaBackend.Api.Services;

public class RealtimeMessageSender<THub>(
    IHubContext<THub> context,
    ILifetimeScope scope) : IRealtimeMessageSender<THub>
    where THub : Hub
{
    private readonly IHubContext<THub> _context = context;
    private readonly ILifetimeScope _scope = scope;

    private static InvalidCastException IdentifierException => new("Couldn't cast Message.Identifier");

    public async Task SendAsync<TResponse, TMessage, TIdentifier>(TResponse response, IModuleContract module)
        where TResponse : IRealtimeResponse<TMessage, TIdentifier>
        where TMessage : IRealtimeMessage
    {
        var SendMessageAsync = response.Recipients switch
        {
            Recipients.SingleUser => _context.Clients
                .User(response.ReceiverId as string ?? throw IdentifierException)
                .SendAsync(response.Method, response.Message),

            Recipients.MultipleUsers => _context.Clients
                .Users(response.ReceiverId as IEnumerable<string> ?? throw IdentifierException)
                .SendAsync(response.Method, response.Message),

            Recipients.Group => _context.Clients
                .Group(response.ReceiverId as string ?? throw IdentifierException)
                .SendAsync(response.Method, response.Message),

            _ => throw new ThisWillNeverHappenException()
        };

        await SendMessageAsync;

        using (var scope = _scope.BeginLifetimeScope())
        {
            var parameters = new List<Parameter>
            {
                new TypedParameter(typeof(TMessage), response.Message),
                new TypedParameter(typeof(IHubContext<THub>), _context)
            };

            var sideEffect = scope.ResolveOptional<IRealtimeSideEffect<TMessage>>(parameters);

            if (sideEffect is null)
            {
                return;
            }

            await module.Publish(sideEffect);
        }
    }

    public async Task SendAllAsync<TResponse, TMessage>(TResponse response)
        where TResponse : IRealtimeResponse<TMessage>
        where TMessage : IRealtimeMessage
    {
        await _context.Clients.All.SendAsync(response.Method, response.Message);
    }
}
