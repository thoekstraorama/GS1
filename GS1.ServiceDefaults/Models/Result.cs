using GS1.ServiceDefaults.Models.Enums;

namespace GS1.ServiceDefaults.Models;

/*
 * Classes om result pattern toe te passen. 
 * Er bestaat een result met en zonder 'Value'. 
 * Zonder value wordt in deze code nog niet gebruikt.
 * Er zijn static methods aanwezig zodat een result altijd in de juiste staat wordt aangemaakt.
*/
public class Result
{
    internal Result(ResultType type)
    {
        Type = type;
    }

    internal Result(ResultType type, string errorMessage)
    {
        Type = type;
        ErrorMessage = errorMessage;
    }

    internal Result(ResultType type, IDictionary<string, string[]> validationErrors)
    {
        Type = type;
        ValidationErrors = validationErrors;
    }

    public ResultType Type { get; }

    public string? ErrorMessage { get; }

    public IDictionary<string, string[]>? ValidationErrors { get; }


    public static Result Success() => new(ResultType.Success);

    public static Result<T> Success<T>(T value) => new(ResultType.Success, value);

    public static Result Forbidden(string errorMessage) => new(ResultType.Forbidden, errorMessage);

    public static Result<T> Forbidden<T>(string errorMessage) => new(ResultType.Forbidden, errorMessage);

    public static Result NotFound(string errorMessage) => new(ResultType.NotFound, errorMessage);

    public static Result<T> NotFound<T>(string errorMessage) => new(ResultType.NotFound, errorMessage);

    public static Result ConflictingEntity(string errorMessage) => new(ResultType.Conflict, errorMessage);

    public static Result<T> ConflictingEntity<T>(string errorMessage) => new(ResultType.Conflict, errorMessage);

    public static Result ValidationFailed(IDictionary<string, string[]> validationErrors) => new(ResultType.ValidationFailed, validationErrors);

    public static Result<T> ValidationFailed<T>(IDictionary<string, string[]> validationErrors) => new(ResultType.ValidationFailed, validationErrors);

    public static Result<T> Failure<T>(string errorMessage) => new(ResultType.Forbidden, errorMessage);

    public static Result Failure(string errorMessage) => new(ResultType.Forbidden, errorMessage);
}

public class Result<TValue> : Result
{
    public TValue? Value { get; }

    internal Result(ResultType type, TValue value) : base(type)
    {
        Value = value;
    }

    internal Result(ResultType type, string errorMessage) : base(type, errorMessage) { }

    internal Result(ResultType type, IDictionary<string, string[]> validationErrors) : base(type, validationErrors) { }
}
