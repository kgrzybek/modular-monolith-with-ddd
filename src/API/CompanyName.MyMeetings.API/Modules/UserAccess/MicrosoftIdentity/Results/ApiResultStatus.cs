namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Results;

public enum ApiResultStatus
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
    /// Invalid error result.
    /// </summary>
    Invalid,

    /// <summary>
    /// Forbidden error result.
    /// </summary>
    Forbidden
}