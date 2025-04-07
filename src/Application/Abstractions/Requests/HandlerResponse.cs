namespace SocialMediaBackend.Application.Abstractions.Requests;

public class HandlerResponse
{
    public bool IsSuccess { get; protected set; }
    public string Message { get; protected set; } = string.Empty;

    internal HandlerResponse() => IsSuccess = true;

    internal HandlerResponse(string message, params object[] parameters)
    {
        IsSuccess = false;
        Message = string.Format(message, parameters);
    }

    internal static HandlerResponse CreateSuccess()
    {
        return new HandlerResponse();
    }

    internal static HandlerResponse CreateError(string message, params object[] parameters)
    {
        return new HandlerResponse(message, parameters);
    }
}

public sealed class HandlerResponse<TResponse> : HandlerResponse
{
    internal HandlerResponse(TResponse payload)
    {
        Payload = payload;
        IsSuccess = true;
    }

    internal HandlerResponse(string message, params object[] parameters) : base(message, parameters) { }

    public TResponse Payload { get; private set; } = default!;

    internal static HandlerResponse<TResponse> CreateSuccess(TResponse payload)
    {
        return new HandlerResponse<TResponse>(payload);
    }

    internal new static HandlerResponse<TResponse> CreateError(string message, params object[] parameters)
    {
        return new HandlerResponse<TResponse>(message, parameters);
    }

    public static implicit operator HandlerResponse<TResponse>(TResponse value) => CreateSuccess(value);
}
