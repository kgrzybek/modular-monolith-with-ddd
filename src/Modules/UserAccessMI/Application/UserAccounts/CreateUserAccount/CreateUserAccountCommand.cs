using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.CreateUserAccount;

public class CreateUserAccountCommand : CommandBase<Result<Guid>>
{
    public CreateUserAccountCommand(string userName, string? password, string? name, string? firstName, string? lastName, string? emailAddress)
    {
        UserName = userName;
        Password = password;
        Name = name;
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
    }

    public string UserName { get; }

    public string? Password { get; }

    public string? Name { get; }

    public string? FirstName { get; }

    public string? LastName { get; }

    public string? EmailAddress { get; }
}