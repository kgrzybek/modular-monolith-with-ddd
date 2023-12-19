namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;

public enum ResultStatus
{
    /// <summary>
    /// Successful result.
    /// </summary>
    Ok,

    /// <summary>
    /// General error result.
    /// </summary>
    Error,

    /// <summary>
    /// Not found error result.
    /// </summary>
    NotFound,

    /// <summary>
    /// Forbidden error result.
    /// </summary>
    Forbidden
}