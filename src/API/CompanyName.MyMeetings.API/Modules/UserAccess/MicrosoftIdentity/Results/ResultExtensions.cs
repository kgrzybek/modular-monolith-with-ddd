using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Results;

internal static class ResultExtensions
{
    /// <summary>
    /// Convert an Result to a ApiResult.
    /// </summary>
    /// <param name="result">The Result to convert.</param>
    /// <returns>ApiResult.</returns>
    public static ApiResult ToApiResult(this MyMeetings.Modules.UserAccessMI.Application.Configuration.Results.IResult result)
    {
        if (result == null)
        {
            return ApiResult.Error();
        }

        // Create the result
        ApiResult apiResult = null;

        // Translate the response
        if (result.Status == ResultStatus.Forbidden)
        {
            apiResult = ApiResult.Forbidden("Not authorized.");
        }

        if (result.Status == ResultStatus.NotFound)
        {
            apiResult = ApiResult.NotFound(result.Errors);
        }

        if (result.Status == ResultStatus.Error)
        {
            apiResult = ApiResult.Error(result.Errors);
        }

        if (result.Status == ResultStatus.Ok)
        {
            apiResult = ApiResult.Ok();
        }

        if (apiResult is null)
        {
            throw new InvalidOperationException($"The Result with status '{result.Status}' cannot be translated to an equivalent ApiResult.");
        }

        return apiResult;
    }

    /// <summary>
    /// Convert an Result to a ApiResult.
    /// </summary>
    /// <typeparam name="T">The value type being returned.</typeparam>
    /// <param name="result">The Result to convert.</param>
    /// <param name="value">The value being returned.</param>
    /// <returns>ApiResult.</returns>
    public static ApiResult ToApiResult<T>(this MyMeetings.Modules.UserAccessMI.Application.Configuration.Results.IResult result, T value)
    {
        if (result == null)
        {
            return ApiResult.Error();
        }

        // Create the result
        ApiResult apiResult = null;

        // Translate the response
        if (result.Status == ResultStatus.Forbidden)
        {
            apiResult = ApiResult.Forbidden("Not authorized.");
        }

        if (result.Status == ResultStatus.NotFound)
        {
            apiResult = ApiResult.NotFound(result.Errors);
        }

        if (result.Status == ResultStatus.Error)
        {
            apiResult = ApiResult.Error(result.Errors);
        }

        if (result.Status == ResultStatus.Ok)
        {
            apiResult = ApiResult.Ok(value);
        }

        if (apiResult is null)
        {
            throw new InvalidOperationException($"The Result with status '{result.Status}' cannot be translated to an equivalent ApiResult.");
        }

        return apiResult;
    }

    /// <summary>
    /// Convert an Result to a ApiResult.
    /// </summary>
    /// <typeparam name="T">The value being returned.</typeparam>
    /// <param name="result">The Result to convert.</param>
    /// <returns>ApiResult.</returns>
    public static ApiResult ToApiResult<T>(this IResult<T> result)
    {
        if (result == null)
        {
            return ApiResult.Error();
        }

        // Create the result
        ApiResult apiResult = null;

        // Translate the response
        if (result.Status == ResultStatus.Forbidden)
        {
            apiResult = ApiResult.Forbidden("Not authorized.");
        }

        if (result.Status == ResultStatus.NotFound)
        {
            apiResult = ApiResult.NotFound(result.Errors);
        }

        if (result.Status == ResultStatus.Error)
        {
            apiResult = ApiResult.Error(result.Errors);
        }

        if (result.Status == ResultStatus.Ok)
        {
            apiResult = ApiResult.Ok(result.Value);
        }

        if (apiResult is null)
        {
            throw new InvalidOperationException($"The Result with status '{result.Status}' cannot be translated to an equivalent ApiResult.");
        }

        return apiResult;
    }
}