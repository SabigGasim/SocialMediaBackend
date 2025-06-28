using Autofac;
using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Contracts;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

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
        var task = response.Recipients switch
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

            _ => throw new InvalidOperationException("")
        };

        await task;

        using (var scope = _scope.BeginLifetimeScope())
        {
            var sideEffect = _scope.ResolveOptional(typeof(IRealtimeSideEffect<TMessage>),
                new TypedParameter(typeof(TMessage), response.Message));

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
