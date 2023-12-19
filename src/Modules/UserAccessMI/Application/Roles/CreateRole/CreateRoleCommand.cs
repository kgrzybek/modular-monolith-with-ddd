using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.CreateRole;

public class CreateRoleCommand : CommandBase<Result<Guid>>
{
    public CreateRoleCommand(string name, IEnumerable<string>? permissions)
    {
        Name = name;
        Permissions = permissions;
    }

    public string Name { get; }

    public IEnumerable<string>? Permissions { get; }
}