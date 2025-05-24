
namespace SocialMediaBackend.BuildingBlocks.Domain;

public enum FailureCode
{
    NotFound = 404
}

public class Result
{
    protected Result() => IsSuccess = true;

    protected Result(FailureCode code, string[] objects)
    {
        IsSuccess = false;
        FailureStatusCode = code;
        Message = GetErrorMessage(code, objects);
    }

    public bool IsSuccess { get; }
    public FailureCode FailureStatusCode { get; }
    public string Message { get; private set; } = default!;

    public static Result Success() => new();
    public static Result Failure(FailureCode code, params string[] objects) => new(code, objects);

    private static string GetErrorMessage(FailureCode code, string[] objects)
    {
        var plural = objects.Length > 1;

        return code switch
        {
            FailureCode.NotFound => $"{string.Join(", ", objects)} {(plural ? "was" : "were")} not found",
            _ => throw new ArgumentOutOfRangeException(nameof(code))
        };
    }
}


public class Result<T> : Result
{
    private Result(T payload) : base()
    {
        Payload = payload;
    }

    private Result(FailureCode code, string[] objects) : base(code, objects) { }

    public static Result<T> Success(T payload) => new(payload);
    public new static Result<T> Failure(FailureCode code, params string[] objects) => new(code, objects);

    public T Payload { get; } = default!;
}