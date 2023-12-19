using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application;

internal static class IdentityHelpers
{
    public static List<Error> Map(this IEnumerable<IdentityError> errors)
    {
        return errors.Select(x => new Error(x.Code, x.Description)).ToList();
    }
}