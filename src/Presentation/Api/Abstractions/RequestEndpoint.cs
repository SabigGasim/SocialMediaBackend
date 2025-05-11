using SocialMediaBackend.Modules.Users.Api.Mappings;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Contracts;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;

namespace SocialMediaBackend.Api.Abstractions;

public class RequestEndpoint<TRequest> : FastEndpoints.Endpoint<TRequest> where TRequest : notnull
{
    protected async Task HandleCommandAsync<TCommand>(
        TCommand command,
        IModuleContract module,
        CancellationToken cancellationToken)
        where TCommand : ICommand<HandlerResponse>
    {
        var handlerResponse = await module.ExecuteCommandAsync(command, cancellationToken);

        if (handlerResponse.IsSuccess)
        {
            await SendNoContentAsync(cancellationToken);
            return;
        }

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: cancellationToken);
    }

    protected Task HandleCommandAsync<T>(ICommand<T> command, CancellationToken cancellationToken)
        where T : IHandlerResponse
    {
        throw new NotImplementedException();
    }
}

public class RequestEndpoint<TRequest, TResponse> : FastEndpoints.Endpoint<TRequest, TResponse>
    where TRequest : notnull
{
    protected async Task HandleQueryAsync<TQuery>(
        TQuery query,
        IModuleContract module,
        CancellationToken ct)
        where TQuery : IQuery<HandlerResponse<TResponse>>
    {
        var handlerResponse = await module.ExecuteQueryAsync<TQuery, TResponse>(query, ct);

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload, (int)statusCode, cancellation: ct);
            return;
        }

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }

    protected async Task HandleCommandAsync<TCommand>(
        TCommand query,
        IModuleContract module,
        CancellationToken ct)
        where TCommand : ICommand<HandlerResponse<TResponse>>
    {
        var handlerResponse = await module.ExecuteCommandAsync<TCommand, TResponse>(query, ct);

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload, (int)statusCode, cancellation: ct);
            return;
        }

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }

    protected Task HandleRequestAsync<T>(IRequest<T> request, CancellationToken cancellationToken)
        where T : IHandlerResponse<TResponse>
    {
        throw new NotImplementedException();
    }
}

public class RequestEndpointWithoutRequest<TResponse> : FastEndpoints.EndpointWithoutRequest<TResponse>
{
    protected async Task HandleCommandAsync<TCommand>(
        TCommand query,
        IModuleContract module,
        CancellationToken ct)
        where TCommand : ICommand<HandlerResponse<TResponse>>
    {
        var handlerResponse = await module.ExecuteCommandAsync<TCommand, TResponse>(query, ct);

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload, (int)statusCode, cancellation: ct);
            return;
        }

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: ct);
    }

    protected Task HandleRequestAsync<T>(IRequest<T> request, CancellationToken cancellationToken)
        where T : IHandlerResponse<TResponse>
    {
        throw new NotImplementedException();
    }
}
