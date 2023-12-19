using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;

/// <summary>
/// Class that represents an error.
///
/// Errors should not be instantiated arbitrarily, but should be taken from a predefined set of errors.
/// Error messages should not be handled by the domain layer. This is an application concern as you might
/// want to translate them based on user settings.
/// Further more as the Error class is part of the domain layer this is technically a violation of domain model
/// purity and as said it shouldn't deal with application concerns. But this is a minor concession.
/// </summary>
public class Error : ValueObject, ICombine
{
    private const string SEPARATOR = "||";

    private readonly List<Error>? _errors;

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class.
    /// </summary>
    /// <param name="code">Error code.</param>
    /// <param name="message">Error message.</param>
    public Error(string code, string? message)
        : this()
    {
        Code = code;
        Message = message;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class.
    /// </summary>
    internal Error()
        : base()
    {
    }

    internal Error(IEnumerable<Error> errors)
        : this()
    {
        if (errors == null)
        {
            throw new ArgumentNullException(nameof(errors));
        }

        _errors = new List<Error>(errors);
    }

    /// <summary>
    /// Error code.
    /// The code is going to be part of the contract between the API and it's clients.
    /// Once published error codes should not be changed.
    /// </summary>
    public string Code { get; } = null!;

    /// <summary>
    /// The error message.
    /// Error messages are just for informational purposes. So we can specify some additional information for debugging.
    /// Ideally the client should not show that message to the end user and instead should map there own error messages
    /// onto the code.
    /// </summary>
    public string? Message { get; }

    [JsonIgnore]
    public IReadOnlyCollection<Error>? Errors => _errors;

    public ICombine Combine(ICombine value)
    {
        if (value is Error error)
        {
            var errorList = new List<Error>();

            if (!string.IsNullOrEmpty(error.Code))
            {
                errorList.Add(new Error(error.Code, error.Message));
            }

            if (error._errors is not null)
            {
                errorList.AddRange(error._errors);
            }

            if (!string.IsNullOrEmpty(Code))
            {
                errorList.Add(new Error(Code, Message));
            }

            if (_errors is not null)
            {
                errorList.AddRange(_errors);
            }

            return new Error(errorList);
        }

        return this;
    }

    public string Serialize() => $"{Code}{SEPARATOR}{Message}";

    public static Error Deserialize(string serialized)
    {
        if (serialized == "A non-empty request body is required.")
        {
            return ErrorHandling.Errors.General.ValueIsRequired();
        }

        string[] data = serialized.Split(new[] { SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
        if (data.Length < 2)
        {
            throw new ArgumentException($"Invalid error serialization: '{serialized}'");
        }

        return new Error(data[0], data[1]);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        // Only the code field is required as the code will be part of the contract between the API and it's clients.
        yield return Code;
    }
}