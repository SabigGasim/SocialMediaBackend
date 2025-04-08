using FastEndpoints;
using SocialMediaBackend.Application.Abstractions.Requests;
using AppCommandAbstractions = SocialMediaBackend.Application.Abstractions.Requests.Commands;

namespace SocialMediaBackend.Api.Abstractions;

public class RequestEndpoint<TRequest> : Endpoint<TRequest> where TRequest : notnull
{
    protected async Task HandleCommandAsync(AppCommandAbstractions.ICommand<HandlerResponse> command, CancellationToken cancellationToken)
    {
        var handlerResponse = await command.ExecuteAsync(cancellationToken);

        if (handlerResponse.IsSuccess)
        {
            await SendNoContentAsync(cancellationToken);
            return;
        }

        AddError(handlerResponse.Message);

        await SendErrorsAsync(cancellation: cancellationToken);
    }
}

public class RequestEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse> where TRequest : notnull
{
    protected async Task HandleRequestAsync(IRequest<HandlerResponse<TResponse>> request, CancellationToken cancellationToken)
    {
        var handlerResponse = await request.ExecuteAsync(cancellationToken);

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload, cancellation: cancellationToken);
            return;
        }

        AddError(handlerResponse.Message);

        await SendErrorsAsync(cancellation: cancellationToken);
    }
}
