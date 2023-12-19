using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;

public interface IResult
{
    ResultStatus Status { get; }

    IReadOnlyList<Error> Errors { get; }

    bool HasError { get; }

    IResult AddError(Error error);

    IResult Forbidden();

    IResult Forbidden(string? message);

    IResult Forbidden(Error error);

    IResult NotFound();

    IResult NotFound(string message);

    IResult NotFound(Error error);
}