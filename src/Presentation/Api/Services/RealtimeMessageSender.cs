using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.BuildingBlocks.Application.Requests;

namespace SocialMediaBackend.Api.Services;

public class RealtimeMessageSender<THub>(IHubContext<THub> context) : IRealtimeMessageSender<THub>
    where THub : Hub
{
    private readonly IHubContext<THub> _context = context;

    private static InvalidCastException IdentifierException => new("Couldn't cast Message.Identifier");

    public async Task SendAsync<TResponse, TMessage, TIdentifier>(TResponse response)
        where TResponse : IRealtimeResponse<TMessage, TIdentifier>
    {
        var task = response.Recipients switch
        {
            Recipients.SingleUser => _context.Clients
                .User(response.Identifier as string ?? throw IdentifierException)
                .SendAsync(response.Method, response.Message),

            Recipients.MultipleUsers => _context.Clients
                .Users(response.Identifier as IEnumerable<string> ?? throw IdentifierException)
                .SendAsync(response.Method, response.Message),

            Recipients.Group => _context.Clients
                .Group(response.Identifier as string ?? throw IdentifierException)
                .SendAsync(response.Method, response.Message),

            _ => throw new InvalidOperationException("")
        };

        await task;
    }

    public async Task SendAllAsync<TResponse, TMessage>(TResponse response)
        where TResponse : IRealtimeResponse<TMessage>
    {
        await _context.Clients.All.SendAsync(response.Method, response.Message);
    }
}
