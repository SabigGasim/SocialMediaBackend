
namespace SocialMediaBackend.BuildingBlocks.Domain;

public enum FailureCode
{
    NotFound = 1,
    Duplicate = 2,
    Conflict = 3,
    Forbidden = 4,
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

    protected Result(FailureCode code, string message)
    {
        IsSuccess = false;
        FailureStatusCode = code;
        Message = message;
    }

    public bool IsSuccess { get; }
    public FailureCode FailureStatusCode { get; }
    public string? Message { get; private set; }

    public static Result Success() => new();
    public static Result Failure(FailureCode code, params string[] objects) => new(code, objects);
    public static Result FailureWithMessage(FailureCode code, string message) => new(code, message);

    private static string GetErrorMessage(FailureCode code, string[] objects)
    {
        var plural = objects.Length > 1;

        return code switch
        {
            FailureCode.NotFound => $"{string.Join(", ", objects)} {(plural ? "was" : "were")} not found",
            FailureCode.Duplicate => $"{string.Join(", ", objects)} already exist{(plural ? "" : "s")}",
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

    private Result(FailureCode code, string message) : base(code, message) { }

    public T Payload { get; } = default!;

    public static Result<T> Success(T payload) => new(payload);
    public new static Result<T> Failure(FailureCode code, params string[] objects) => new(code, objects);
    public new static Result<T> FailureWithMessage(FailureCode code, string message) => new(code, message);

    public static implicit operator Result<T>(T payload) => new(payload);
}