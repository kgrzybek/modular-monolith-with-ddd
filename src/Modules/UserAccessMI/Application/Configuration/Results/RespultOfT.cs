using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CSharpFunctionalExtensions;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;

#pragma warning disable SA1649 // File name should match first type name
public class Result<T> : Result, IResult<T>
#pragma warning restore SA1649 // File name should match first type name
{
    public Result()
        : base()
    {
    }

    public Result(T value)
        : this()
    {
        Value = value;
    }

    public T? Value { get; set; }

    public static implicit operator Result<T>(T result)
        => Result<T>.Ok(result);

    public static implicit operator Result<T>(Error error)
        => Result<T>.Error(error);

    public static implicit operator Result<T>(Result<T, Error> result)
        => result.IsSuccess ? Result.Ok<T>(result.Value) : Result.Error<T>(result.Error);

    public static Result<T> Ok(T value)
        => new Result<T>(value);

    public static new Result<T> Error(Error error)
    {
        var response = new Result<T>();
        response.AddError(error);

        return response;
    }
}