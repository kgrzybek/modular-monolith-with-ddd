namespace CompanyName.MyMeetings.Contracts.Results;

public class Result
{
    public object? Value { get; set; }

    public DateTime TimeGenerated { get; set; }

    public IDictionary<string, IEnumerable<ErrorMessage>>? Errors { get; set; }

    public Result()
    {
    }

    internal Result(object? value, IDictionary<string, IEnumerable<ErrorMessage>>? errors)
    {
        Value = value;
        Errors = errors;
        TimeGenerated = DateTime.UtcNow;
    }

    public static Result Ok()
        => Ok(null);

    public static Result Ok(object? value)
        => new Result(value, null);

    public static Result Error(ErrorMessage error, string? invalidField = null)
        => Error(new Dictionary<string, IEnumerable<ErrorMessage>>() { { invalidField ?? string.Empty, new[] { error } } });

    public static Result Error(IDictionary<string, IEnumerable<ErrorMessage>> errors)
        => new Result(null, errors);

    public static Result Error(IEnumerable<ErrorMessage> errors, string? invalidField = null)
        => new Result(null, new Dictionary<string, IEnumerable<ErrorMessage>>() { { invalidField ?? string.Empty, errors } });

    public static Result<T> Ok<T>()
        where T : notnull
        => new Result<T>(default, null);

    public static Result<T> Ok<T>(T? value)
        where T : notnull
        => new Result<T>(value, null);

    public static Result<T> Error<T>(IDictionary<string, IEnumerable<ErrorMessage>> errors)
        where T : notnull
        => new Result<T>(default, errors);
}