using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CSharpFunctionalExtensions;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;

public class Result : IResult
{
    private readonly IList<Error> _errors = new List<Error>();

    public Result()
    {
        Status = ResultStatus.Ok;
    }

    public ResultStatus Status { get; protected set; }

    public IReadOnlyList<Error> Errors => _errors.AsReadOnly();

    public bool HasError => _errors.Count > 0;

    public IResult AddError(Error error)
    {
        if (error == null)
        {
            throw new ArgumentNullException(nameof(error), "Error should not be empty.");
        }

        if (error.Errors != null)
        {
            foreach (var errorObj in error.Errors)
            {
                _errors.Add(errorObj);
            }
        }

        if (error.Code != null)
        {
            _errors.Add(error);
        }

        Status = ResultStatus.Error;

        return this;
    }

    public IResult Forbidden() =>
        Forbidden(string.Empty);

    public IResult Forbidden(string? message) =>
        Forbidden(Domain.ErrorHandling.Errors.Authentication.NotAuthorized(message?.Trim() ?? string.Empty));

    public IResult Forbidden(Error error)
    {
        AddError(error);

        Status = ResultStatus.Forbidden;
        return this;
    }

    public IResult NotFound() =>
        NotFound(Domain.ErrorHandling.Errors.General.NotFound());

    public IResult NotFound(string message) =>
        NotFound(Domain.ErrorHandling.Errors.General.NotFound(message));

    public IResult NotFound(long id) =>
        NotFound(Domain.ErrorHandling.Errors.General.NotFound(id));

    public static IResult NotFound(long id, string item)
    {
        var response = new Result();
        response.NotFound(Domain.ErrorHandling.Errors.General.NotFound(id, item));
        return response;
    }

    public IResult NotFound(Error error)
    {
        AddError(error);

        Status = ResultStatus.NotFound;
        return this;
    }

    public static Result Error(Error error)
    {
        var response = new Result();
        response.AddError(error);

        return response;
    }

    public static Result<T> Error<T>(Error error)
    {
        var response = new Result<T>();
        response.AddError(error);

        return response;
    }

    public static Result Ok() =>
        new Result();

    public static Result<T> Ok<T>() =>
        new Result<T>();

    public static Result<T> Ok<T>(T value) =>
        new Result<T>(value);

    public static implicit operator Result(Error error) =>
        Result.Error(error);

    public static implicit operator Result(UnitResult<Error> result) =>
        result.IsSuccess ? Result.Ok() : Result.Error(result.Error);

    public static implicit operator Result(Result<object, Error> result) =>
        result.IsSuccess ? Result.Ok(result.Value) : Result.Error(result.Error);
}