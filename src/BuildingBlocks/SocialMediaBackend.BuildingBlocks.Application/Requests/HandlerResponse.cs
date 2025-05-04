namespace SocialMediaBackend.BuildingBlocks.Application.Requests;

public interface IHandlerResponse
{
    static abstract TResult CreateError<TResult>(string message, HandlerResponseStatus status, params object[] parameters)
        where TResult : IHandlerResponse;
    bool IsSuccess { get; }
    string Message { get; }
    HandlerResponseStatus ResponseStatus { get; }
}

public interface IHandlerResponse<T> : IHandlerResponse
{
    T Payload { get; }
}

public class HandlerResponse : IHandlerResponse
{
    public bool IsSuccess { get; protected set; }
    public string Message { get; protected set; } = string.Empty;
    public HandlerResponseStatus ResponseStatus { get; protected set; }

    internal HandlerResponse(HandlerResponseStatus responseStatus)
    {
        IsSuccess = true;
        ResponseStatus = responseStatus;
    }

    internal HandlerResponse(string message, HandlerResponseStatus responseStatus, params object[] parameters)
    {
        IsSuccess = false;
        ResponseStatus = responseStatus;
        Message = string.Format(message, parameters);
    }

    internal static HandlerResponse CreateSuccess(HandlerResponseStatus responseStatus = HandlerResponseStatus.OK)
    {
        return new HandlerResponse(responseStatus);
    }

    internal static HandlerResponse CreateError(string message, HandlerResponseStatus responseStatus, params object[] parameters)
    {
        return new HandlerResponse(message, responseStatus, parameters);
    }

    public static TResult CreateError<TResult>(string message, HandlerResponseStatus status, params object[] parameters)
        where TResult : IHandlerResponse
    {
        return (TResult)(IHandlerResponse)CreateError(message, status, parameters);
    }

    public static implicit operator HandlerResponse(HandlerResponseStatus responseStatus) => CreateSuccess(responseStatus);

    public static implicit operator HandlerResponse((string message, HandlerResponseStatus responseStatus) response)
        => CreateError(response.message, response.responseStatus);

    public static implicit operator HandlerResponse((string message, HandlerResponseStatus responseStatus, object param) response)
        => CreateError(response.message, response.responseStatus, response.param);

    public static implicit operator HandlerResponse((string message, HandlerResponseStatus responseStatus, object[] parameters) response)
        => CreateError(response.message, response.responseStatus, response.parameters);
}

public sealed class HandlerResponse<TResponse> : HandlerResponse, IHandlerResponse<TResponse>
{
    internal HandlerResponse(TResponse payload, HandlerResponseStatus responseStatus) : base(responseStatus)
    {
        Payload = payload;
    }

    internal HandlerResponse(string message, HandlerResponseStatus responseStatus, params object[] parameters)
        : base(message, responseStatus, parameters) { }

    public TResponse Payload { get; private set; } = default!;

    internal static HandlerResponse<TResponse> CreateSuccess(TResponse payload, HandlerResponseStatus responseStatus = HandlerResponseStatus.OK)
    {
        return new HandlerResponse<TResponse>(payload, responseStatus);
    }

    internal new static HandlerResponse<TResponse> CreateError(string message, HandlerResponseStatus responseStatus, params object[] parameters)
    {
        return new HandlerResponse<TResponse>(message, responseStatus, parameters);
    }

    public new static TResult CreateError<TResult>(string message, HandlerResponseStatus responseStatus, params object[] parameters)
        where TResult : IHandlerResponse
    {
        return (TResult)(IHandlerResponse)CreateError(message, responseStatus, parameters);
    }

    public static implicit operator HandlerResponse<TResponse>(TResponse value) => CreateSuccess(value);
    public static implicit operator HandlerResponse<TResponse>((TResponse value, HandlerResponseStatus responseStatus) response)
        => CreateSuccess(response.value, response.responseStatus);

    public static implicit operator HandlerResponse<TResponse>((string message, HandlerResponseStatus responseStatus, object param) response)
        => HandlerResponse<TResponse>.CreateError(response.message, response.responseStatus, response.param);

    public static implicit operator HandlerResponse<TResponse>((string message, HandlerResponseStatus responseStatus, object[] parameters) response)
        => HandlerResponse<TResponse>.CreateError(response.message, response.responseStatus, response.parameters);

    public static implicit operator HandlerResponse<TResponse>((string message, HandlerResponseStatus responseStatus) response)
        => HandlerResponse<TResponse>.CreateError(response.message, response.responseStatus);

    public void Deconstruct(out HandlerResponse response, out TResponse result)
    {
        response = this as HandlerResponse;
        result = Payload;
    }
}
