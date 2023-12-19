using CompanyName.MyMeetings.Contracts.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Results;

internal static class ErrorMapper
{
    public static IDictionary<string, IEnumerable<ErrorMessage>> ToErrorMessages(this IDictionary<string, IEnumerable<Error>> errors)
    {
        var errorMessages = new Dictionary<string, IEnumerable<ErrorMessage>>();
        foreach (var keyValue in errors)
        {
            errorMessages.Add(keyValue.Key, keyValue.Value.ToErrorMessages());
        }

        return errorMessages;
    }

    public static IEnumerable<ErrorMessage> ToErrorMessages(this IEnumerable<Error> errors)
    {
        var errorMessages = new List<ErrorMessage>();
        foreach (var error in errors)
        {
            errorMessages.AddRange(error.ToErrorMessages());
        }

        return errorMessages;
    }

    public static IEnumerable<ErrorMessage> ToErrorMessages(this Error error, List<ErrorMessage> errorMessages = null)
    {
        if (error is null)
        {
            throw new ArgumentNullException(nameof(error));
        }

        errorMessages ??= new List<ErrorMessage>();

        if (!string.IsNullOrEmpty(error.Code + error.Message))
        {
            errorMessages.Add(error.ToErrorMessage());
        }

        if (error.Errors is not null)
        {
            foreach (var subError in error.Errors)
            {
                subError.ToErrorMessages(errorMessages);
            }
        }

        return errorMessages;
    }

    private static ErrorMessage ToErrorMessage(this Error error)
    {
        if (error is null)
        {
            throw new ArgumentNullException(nameof(error));
        }

        return new ErrorMessage(error.Code, error.Message);
    }
}
