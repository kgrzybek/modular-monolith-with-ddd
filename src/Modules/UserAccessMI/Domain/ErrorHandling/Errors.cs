namespace CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;

/// <summary>
/// This class will enumerate all possible errors of the application.
///
/// As the Error class, this class is an other violation in domain model purity,
/// because we are combining all errors in one list including errors that don't
/// belong to the domain layer. Technically those should be separated into domain
/// and none domain errors. But this is a small concession and it's more useful to keep the
/// full list of errors in one place rather then maintain perfect separation.
/// </summary>
public static partial class Errors
{
    public static class General
    {
        public static Error NotFound() =>
            new Error("record.not.found", "Record not found.");

        public static Error NotFound(string message) =>
            new Error("record.not.found", message);

        public static Error NotFound(long id) =>
            new Error("record.not.found", $"Record not found for Id '{id}'.");

        public static Error NotFound(long id, string itemName) =>
            new Error("record.not.found", $"{itemName} not found for Id '{id}'.");

        public static Error NotFound(Guid id) =>
            new Error("record.not.found", $"Record not found for Id '{id}'.");

        public static Error NotFound(Guid id, string itemName) =>
            new Error("record.not.found", $"{itemName} not found for Id '{id}'.");

        public static Error NotFound(string value, string itemName) =>
            new Error("record.not.found", $"{itemName} not found for value '{value}'.");

        public static Error ValueIsInvalid() =>
            new Error("value.is.invalid", "Value is invalid.");

        public static Error ValueIsInvalid(string message) =>
            new Error("value.is.invalid", message);

        public static Error ValueIsRequired() =>
            new Error("value.is.required", "Value is required.");

        public static Error ValueIsRequired(string? name = null)
        {
            string label = name == null ? "Value" : name.Trim();
            return new Error("value.is.required", $"{label} is required.");
        }

        public static Error ValueMustBeUnique(string name) =>
            new Error("value.must.be.unique", $"Record already present for Value '{name}'");

        public static Error ValueMustBePositive(string valueName) =>
            new("value.must.be.positive", $"{valueName} must be positive.");

        public static Error ValueMustBeGreaterThan(string valueName, int numberToCompare) =>
            new("value.must.be.greater.than", $"{valueName} must be greater than {numberToCompare}.");

        public static Error ValueMustBeLessThan(string valueName, int numberToCompare) =>
            new("value.must.be.less.than", $"{valueName} must be less than {numberToCompare}.");

        public static Error InvalidLength(string? name = null, int? maxLength = null)
        {
            string label = name == null ? " " : $" {name} ";
            string lengthHint = maxLength == null ? string.Empty : $" The length may not be longer than {maxLength} characters.";
            return new Error("invalid.string.length", $"Invalid{label}length.{lengthHint}");
        }

        public static Error CollectionIsTooSmall(int min, int current) =>
            new Error("collection.is.too.small", $"The collection must contain {min} items or more. It contains {current} items.");

        public static Error CollectionIsTooLarge(int max, int current) =>
            new Error("collection.is.too.large", $"The collection must contain {max} items or less. It contains {current} items.");

        public static Error InternalServerError(string message) =>
            new Error("internal.server.error", message);

        public static Error InvalidRequest() =>
            new Error("invalid.request", "Invalid request");

        public static Error InvalidModel() =>
            new Error("invalid.model", "Invalid model");
    }

    public static class Authentication
    {
        public static Error InvalidToken() =>
            new Error("invalid.token", "Invalid token");

        public static Error InvalidToken(string message) =>
            new Error("invalid.token", message);

        public static Error LoginRequestExpired() =>
            new Error("login.request.expired", "Your login request has expired, please start over.");

        public static Error NotAuthorized() =>
            new Error("not.authorized", "You are not authorized to perform this action.");

        public static Error NotAuthorized(string message) =>
            new Error("not.authorized", message);

        public static Error AuthenticatorKeyNotFound() =>
            new Error("authenticator.key.not.found", "Authenticator key could not be retrieved.");

        public static Error NotAllowed(string? message = null) =>
            new Error("not.allowed", message ?? "You are not allowed to perform this operation.");

        public static Error InvalidTwoFactorAuthenticationToken() =>
            new Error("invalid.two.factor.authentication.token", "Two factor authentication token is invalid.");
    }

    public static class UserAccess
    {
        public static Error InvalidUserNameOrPassword =>
            new Error("invalid.username.or.password", "Invalid UserName or Password.");

        public static Error EmailNotConfirmed =>
            new Error("email.not.confirmed", "Email is not confirmed.");

        public static Error LoginNotAllowed =>
            new Error("login.not.allowed", "Not allowed tp login.");
    }
}