using FastEndpoints;
using SocialMediaBackend.Application.Common.Abstractions.Requests;
using AppCommandAbstractions = SocialMediaBackend.Application.Common.Abstractions.Requests.Commands;

namespace SocialMediaBackend.Api.Abstractions;

public class RequestEndpoint<TRequest> : Endpoint<TRequest> where TRequest : notnull
{
    protected async Task HandleCommandAsync(AppCommandAbstractions.ICommand command, CancellationToken cancellationToken)
    {
        await command.ExecuteAsync(cancellationToken);
        await SendNoContentAsync(cancellationToken);
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
