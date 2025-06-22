using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Contracts;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Api.Mappings;

namespace SocialMediaBackend.Api.Abstractions;

public class RequestEndpoint<TRequest>(IModuleContract module) : FastEndpoints.Endpoint<TRequest>
    where TRequest : notnull
{
    private readonly IModuleContract _module = module;

    protected async Task HandleCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
        where TCommand : ICommand<HandlerResponse>
    {
        var handlerResponse = await _module.ExecuteCommandAsync(command, cancellationToken);

        if (handlerResponse.IsSuccess)
        {
            await SendNoContentAsync(cancellationToken);
            return;
        }

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: cancellationToken);
    }
}

public class RequestEndpoint<TRequest, TResponse>(IModuleContract module) : FastEndpoints.Endpoint<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IModuleContract _module = module;

    protected async Task HandleQueryAsync<TQuery>(TQuery query, CancellationToken ct)
        where TQuery : IQuery<HandlerResponse<TResponse>>
    {
        var handlerResponse = await _module.ExecuteQueryAsync<TQuery, TResponse>(query, ct);

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload, (int)statusCode, cancellation: ct);
            return;
        }

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }

    protected async Task HandleCommandAsync<TCommand>(TCommand command, CancellationToken ct)
        where TCommand : ICommand<HandlerResponse<TResponse>>
    {
        var handlerResponse = await _module.ExecuteCommandAsync<TCommand, TResponse>(command, ct);

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload, (int)statusCode, cancellation: ct);
            return;
        }

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }
}

public class RequestEndpointWithoutRequest<TResponse>(IModuleContract module) : FastEndpoints.EndpointWithoutRequest<TResponse>
{
    private readonly IModuleContract _module = module;

    protected async Task HandleCommandAsync<TCommand>(TCommand command, CancellationToken ct)
        where TCommand : ICommand<HandlerResponse<TResponse>>
    {
        var handlerResponse = await _module.ExecuteCommandAsync<TCommand, TResponse>(command, ct);

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload, (int)statusCode, cancellation: ct);
            return;
        }

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }

    protected async Task HandleQueryAsync<TQuery>(TQuery query, CancellationToken ct)
        where TQuery : IQuery<HandlerResponse<TResponse>>
    {
        var handlerResponse = await _module.ExecuteQueryAsync<TQuery, TResponse>(query, ct);

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload, (int)statusCode, cancellation: ct);
            return;
        }

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }
}


public abstract class RequestEndpoint(IModuleContract module) : FastEndpoints.EndpointWithoutRequest
{
    private readonly IModuleContract _module = module;

    protected async Task HandleCommandAsync<TCommand>(TCommand command, CancellationToken ct)
        where TCommand : ICommand<HandlerResponse>
    {
        var handlerResponse = await _module.ExecuteCommandAsync(command, ct);

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        if (handlerResponse.IsSuccess)
        {
            await SendNoContentAsync(ct);
            return;
        }

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }
}
