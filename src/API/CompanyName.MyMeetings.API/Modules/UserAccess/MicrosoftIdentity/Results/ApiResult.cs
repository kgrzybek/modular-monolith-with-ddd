using System.Net;
using CompanyName.MyMeetings.Contracts.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Results;

public class ApiResult : IActionResult
{
    private readonly Result _result;
    private readonly int _statusCode;

    public ApiResult(Result result, HttpStatusCode statusCode)
    {
        _result = result;
        _statusCode = (int)statusCode;
    }

    public Guid? CorrelationId { get; set; }

    public ApiResultStatus Status => MapStatus(_statusCode);

    public Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new ObjectResult(_result)
        {
            StatusCode = _statusCode
        };

        return objectResult.ExecuteResultAsync(context);
    }

    /*
    public static ApiResult Ok(string successMessage = null)
        => new ApiResult(Result.Ok(successMessage), HttpStatusCode.OK);
    */

    public static ApiResult Ok()
    => new ApiResult(Result.Ok(), HttpStatusCode.OK);

    public static ApiResult Ok(object result = null)
        => new ApiResult(Result.Ok(result), HttpStatusCode.OK);

    /*
    public static ApiResult Ok(object result = null, string successMessage = null)
        => new ApiResult(Result.Ok(result, successMessage), HttpStatusCode.OK);
    */

    public static ApiResult Error(Error error, string fieldName = null)
        => new ApiResult(Result.Error(error.ToErrorMessages(), fieldName), HttpStatusCode.BadRequest);

    public static ApiResult Error()
        => Error(Errors.General.InvalidRequest());

    public static ApiResult Error(Error error)
        => Error(new Dictionary<string, IEnumerable<Error>>() { { string.Empty, new[] { error } } });

    public static ApiResult Error(IEnumerable<Error> errors)
        => Error(new Dictionary<string, IEnumerable<Error>>() { { string.Empty, errors } });

    public static ApiResult Error(IDictionary<string, IEnumerable<Error>> errors)
        => new ApiResult(Result.Error(errors.ToErrorMessages()), HttpStatusCode.BadRequest);

    public static ApiResult NotFound(Error error)
        => NotFound(new Dictionary<string, IEnumerable<Error>>() { { string.Empty, new[] { error } } });

    public static ApiResult NotFound(Error error, string fieldName = null)
        => NotFound(new Dictionary<string, IEnumerable<Error>>() { { fieldName ?? string.Empty, new[] { error } } });

    public static ApiResult NotFound(IEnumerable<Error> errors)
        => NotFound(new Dictionary<string, IEnumerable<Error>>() { { string.Empty, errors } });

    public static ApiResult NotFound(IDictionary<string, IEnumerable<Error>> errors)
        => new ApiResult(Result.Error(errors.ToErrorMessages()), HttpStatusCode.NotFound);

    public static ApiResult Forbidden(string errorMessage)
        => Forbidden(Errors.Authentication.NotAuthorized(errorMessage));

    public static ApiResult Forbidden(Error error)
        => Forbidden(new Dictionary<string, IEnumerable<Error>>() { { string.Empty, new[] { error } } });

    public static ApiResult Forbidden(IEnumerable<Error> errors)
        => Forbidden(new Dictionary<string, IEnumerable<Error>>() { { string.Empty, errors } });

    public static ApiResult Forbidden(IDictionary<string, IEnumerable<Error>> errors)
        => new ApiResult(Result.Error(errors.ToErrorMessages()), HttpStatusCode.Forbidden);

    private ApiResultStatus MapStatus(int status)
    {
        switch (status)
        {
            case (int)HttpStatusCode.InternalServerError:
                return ApiResultStatus.Error;

            case (int)HttpStatusCode.Forbidden:
                return ApiResultStatus.Forbidden;

            case (int)HttpStatusCode.BadRequest:
                return ApiResultStatus.Invalid;

            case (int)HttpStatusCode.NotFound:
                return ApiResultStatus.NotFound;

            case (int)HttpStatusCode.OK:
                return ApiResultStatus.Ok;
        }

        return ApiResultStatus.Error;
    }
}
