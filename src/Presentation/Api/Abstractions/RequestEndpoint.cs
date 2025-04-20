using FastEndpoints;
using SocialMediaBackend.Application.Abstractions.Requests;
using AppCommandAbstractions = SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Api.Mappings;

namespace SocialMediaBackend.Api.Abstractions;

public class RequestEndpoint<TRequest> : Endpoint<TRequest> where TRequest : notnull
{
    protected async Task HandleCommandAsync<T>(AppCommandAbstractions.ICommand<T> command, CancellationToken cancellationToken)
        where T : IHandlerResponse
    {
        var handlerResponse = await command.ExecuteAsync(cancellationToken);

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

public class RequestEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse> 
    where TRequest : notnull
{
    protected async Task HandleRequestAsync<T>(IRequest<T> request, CancellationToken cancellationToken)
        where T : IHandlerResponse<TResponse>
    {
        var handlerResponse = await request.ExecuteAsync(cancellationToken);

        var statusCode = handlerResponse.ResponseStatus.MapToHttpStatusCode();

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload, (int)statusCode, cancellation: cancellationToken);
            return;
        }

        AddError(handlerResponse.Message, statusCode.ToString());

        await SendErrorsAsync((int)statusCode, cancellation: cancellationToken);
    }
}
