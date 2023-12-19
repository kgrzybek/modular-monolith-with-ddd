using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.UpdateUserAccount;

public class UpdateUserAccountCommand : CommandBase<Result>
{
    public UpdateUserAccountCommand(Guid userId, string? name, string? firstName, string? lastName)
    {
        UserId = userId;
        Name = name;
        FirstName = firstName;
        LastName = lastName;
    }

    public Guid UserId { get; }

    public string? Name { get; }

    public string? FirstName { get; }

    public string? LastName { get; }
}