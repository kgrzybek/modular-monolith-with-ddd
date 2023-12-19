using CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity;

/// <summary>
/// Custom base class for API Controllers
///
/// Decorating the controller class with the ApiController attribute unleashes more magic power the framework offers.
/// This automates the model state checks and the model state IsValid property doesn't need to be checked manually.
/// Further more the [FromBody] attribute isn't needed anymore since the controller now automatically infers the binding source for the incoming data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ApplicationController : ControllerBase
{
    protected new IActionResult Ok(object result = null)
    {
        return ApiResult.Ok(result);
    }

    /*
    protected IActionResult Ok(string successMessage, object result = null)
    {
        return ApiResult.Ok(result, successMessage);
    }
    */

    protected IActionResult NotFound(Error error, string invalidField = null)
    {
        return ApiResult.NotFound(error, invalidField);
    }

    protected IActionResult Error(Error error, string invalidField = null)
    {
        return ApiResult.Error(error, invalidField);
    }

    protected IActionResult FromResponse(Result response)
    {
        return response.ToApiResult();
    }

    protected IActionResult FromResult<T>(CSharpFunctionalExtensions.Result<T, Error> result)
    {
        if (result.IsSuccess)
        {
            return Ok();
        }

        return Error(result.Error);
    }
}
