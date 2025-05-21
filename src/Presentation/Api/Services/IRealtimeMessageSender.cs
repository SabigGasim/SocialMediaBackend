using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.BuildingBlocks.Application.Requests;

namespace SocialMediaBackend.Api.Services;

public interface IRealtimeMessageSender<THub>
    where THub : Hub
{
    Task SendAsync<TResponse, TMessage, TIdentifier>(TResponse response) where TResponse : IRealtimeResponse<TMessage, TIdentifier>;
    Task SendAllAsync<TResponse, TMessage>(TResponse response) where TResponse : IRealtimeResponse<TMessage>;
}
