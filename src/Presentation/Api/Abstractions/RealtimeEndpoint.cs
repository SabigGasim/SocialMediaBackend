using SocialMediaBackend.Modules.Users.Api.Mappings;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Contracts;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.Api.Services;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

namespace SocialMediaBackend.Api.Abstractions;

public class RealtimeEndpoint<TRequest, TMessage, THub>(
    IModuleContract module,
    IRealtimeMessageSender<THub> sender) : FastEndpoints.Endpoint<TRequest>
    where TRequest : notnull
    where THub : Hub
    where TMessage : IRealtimeMessage
{
    private readonly IModuleContract _module = module;
    private readonly IRealtimeMessageSender<THub> _sender = sender;

    protected async Task HandleSingleUserCommand<TCommand>(TCommand command, CancellationToken ct)
        where TCommand : ICommand<HandlerResponse<SingleUserResponse<TMessage>>>
    {
        var handlerResponse = await _module.ExecuteCommandAsync<TCommand, SingleUserResponse<TMessage>>(command, ct);

        if (handlerResponse.IsSuccess)
        {
            await SendNoContentAsync(ct);

            await _sender.SendAsync<SingleUserResponse<TMessage>, TMessage, string>(handlerResponse.Payload);

            return;
        }

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }

    protected async Task HandleMultipleUsersCommand<TCommand>(TCommand command, CancellationToken ct)
        where TCommand : ICommand<HandlerResponse<MultipleUsersResponse<TMessage>>>
    {
        var handlerResponse = await _module.ExecuteCommandAsync<TCommand, MultipleUsersResponse<TMessage>>(command, ct);

        if (handlerResponse.IsSuccess)
        {
            await SendNoContentAsync(ct);

            await _sender.SendAsync<MultipleUsersResponse<TMessage>, TMessage, IEnumerable<string>>(handlerResponse.Payload);

            return;
        }

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }
    
    protected async Task HandleAllUsersCommandAsync<TCommand>(TCommand command, CancellationToken ct)
        where TCommand : ICommand<HandlerResponse<AllUsersResponse<TMessage>>>
    {
        var handlerResponse = await _module.ExecuteCommandAsync<TCommand, AllUsersResponse<TMessage>>(command, ct);

        if (handlerResponse.IsSuccess)
        {
            await SendNoContentAsync(ct);

            await _sender.SendAllAsync<AllUsersResponse<TMessage>, TMessage>(handlerResponse.Payload);

            return;
        }

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }
    
    protected async Task HandleGroupCommandAsync<TCommand>(TCommand command, CancellationToken ct)
        where TCommand : ICommand<HandlerResponse<GroupResponse<TMessage>>>
    {
        var handlerResponse = await _module.ExecuteCommandAsync<TCommand, GroupResponse<TMessage>>(command, ct);

        if (handlerResponse.IsSuccess)
        {
            await SendNoContentAsync(ct);

            await _sender.SendAsync<GroupResponse<TMessage>, TMessage, string>(handlerResponse.Payload);

            return;
        }

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }
}

public class RealtimeEndpoint<TRequest, TResponse, TMessage, THub>(
    IModuleContract module,
    IRealtimeMessageSender<THub> sender) : FastEndpoints.Endpoint<TRequest, TResponse>
    where TRequest : notnull
    where THub : Hub
    where TMessage : IRealtimeMessage
{
    private readonly IModuleContract _module = module;
    private readonly IRealtimeMessageSender<THub> _sender = sender;

    protected async Task HandleSingleUserCommandAsync<TCommand>(TCommand command, CancellationToken ct)
        where TCommand : ICommand<HandlerResponse<SingleUserResponse<TMessage, TResponse>>>
    {
        var handlerResponse = await _module.ExecuteCommandAsync<TCommand, SingleUserResponse<TMessage, TResponse>>(command, ct);

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload.Payload, (int)statusCode, cancellation: ct);

            await _sender.SendAsync<SingleUserResponse<TMessage, TResponse>, TMessage, string>(handlerResponse.Payload);

            return;
        }

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }

    protected async Task HandleMultipleUsersCommandAsync<TCommand>(TCommand command, CancellationToken ct)
        where TCommand : ICommand<HandlerResponse<MultipleUsersResponse<TMessage, TResponse>>>
    {
        var handlerResponse = await _module.ExecuteCommandAsync<TCommand, MultipleUsersResponse<TMessage, TResponse>>(command, ct);

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload.Payload, (int)statusCode, cancellation: ct);

            await _sender.SendAsync<MultipleUsersResponse<TMessage, TResponse>, TMessage, IEnumerable<string>>(handlerResponse.Payload);

            return;
        }

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }

    protected async Task HandleGroupCommandAsync<TCommand>(TCommand command, CancellationToken ct)
        where TCommand : ICommand<HandlerResponse<GroupResponse<TMessage, TResponse>>>
    {
        var handlerResponse = await _module.ExecuteCommandAsync<TCommand, GroupResponse<TMessage, TResponse>>(command, ct);

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload.Payload, (int)statusCode, cancellation: ct);

            await _sender.SendAsync<GroupResponse<TMessage, TResponse>, TMessage, string>(handlerResponse.Payload);

            return;
        }

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }

    protected async Task HandleAllUsersCommandAsync<TCommand>(TCommand command, CancellationToken ct)
        where TCommand : ICommand<HandlerResponse<AllUsersResponse<TMessage, TResponse>>>
    {
        var handlerResponse = await _module.ExecuteCommandAsync<TCommand, AllUsersResponse<TMessage, TResponse>>(command, ct);

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload.Payload, (int)statusCode, cancellation: ct);

            await _sender.SendAllAsync<AllUsersResponse<TMessage, TResponse>, TMessage>(handlerResponse.Payload);

            return;
        }

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }
}

public class RealtimeEndpointWithoutRequest<TResponse, TMessage, THub>(
    IModuleContract module,
    IRealtimeMessageSender<THub> sender) : FastEndpoints.EndpointWithoutRequest<TResponse>
    where THub : Hub
    where TMessage : IRealtimeMessage
{
    private readonly IModuleContract _module = module;
    private readonly IRealtimeMessageSender<THub> _sender = sender;

    protected async Task HandleSingleUserCommandAsync<TCommand>(TCommand command, CancellationToken ct)
        where TCommand : ICommand<HandlerResponse<SingleUserResponse<TMessage, TResponse>>>
    {
        var handlerResponse = await _module.ExecuteCommandAsync<TCommand, SingleUserResponse<TMessage, TResponse>>(command, ct);

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload.Payload, (int)statusCode, cancellation: ct);

            await _sender.SendAsync<SingleUserResponse<TMessage, TResponse>, TMessage, string>(handlerResponse.Payload);

            return;
        }

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }

    protected async Task HandleMultipleUsersCommandAsync<TCommand>(TCommand command, CancellationToken ct)
        where TCommand : ICommand<HandlerResponse<MultipleUsersResponse<TMessage, TResponse>>>
    {
        var handlerResponse = await _module.ExecuteCommandAsync<TCommand, MultipleUsersResponse<TMessage, TResponse>>(command, ct);

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload.Payload, (int)statusCode, cancellation: ct);

            await _sender.SendAsync<MultipleUsersResponse<TMessage, TResponse>, TMessage, IEnumerable<string>>(handlerResponse.Payload);

            return;
        }

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }

    protected async Task HandleGroupCommandAsync<TCommand>(TCommand command, CancellationToken ct)
        where TCommand : ICommand<HandlerResponse<GroupResponse<TMessage, TResponse>>>
    {
        var handlerResponse = await _module.ExecuteCommandAsync<TCommand, GroupResponse<TMessage, TResponse>>(command, ct);

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload.Payload, (int)statusCode, cancellation: ct);

            await _sender.SendAsync<GroupResponse<TMessage, TResponse>, TMessage, string>(handlerResponse.Payload);

            return;
        }

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }

    protected async Task HandleAllUsersCommandAsync<TCommand>(TCommand command, CancellationToken ct)
        where TCommand : ICommand<HandlerResponse<AllUsersResponse<TMessage, TResponse>>>
    {
        var handlerResponse = await _module.ExecuteCommandAsync<TCommand, AllUsersResponse<TMessage, TResponse>>(command, ct);

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload.Payload, (int)statusCode, cancellation: ct);

            await _sender.SendAllAsync<AllUsersResponse<TMessage, TResponse>, TMessage>(handlerResponse.Payload);

            return;
        }

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }
}
