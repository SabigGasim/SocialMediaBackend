using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.BuildingBlocks.Application.Contracts;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

namespace SocialMediaBackend.Api.Services;

public interface IRealtimeMessageSender<THub>
    where THub : Hub
{
    Task SendAsync<TResponse, TMessage, TIdentifier>(TResponse respons, IModuleContract modulee) 
        where TResponse : IRealtimeResponse<TMessage, TIdentifier>
        where TMessage : IRealtimeMessage;
    Task SendAllAsync<TResponse, TMessage>(TResponse response)
        where TResponse : IRealtimeResponse<TMessage>
        where TMessage : IRealtimeMessage;
}
