using System.ComponentModel.DataAnnotations;

namespace CompanyName.MyMeetings.Contracts.V1.Users.Users;

public class CreateUserAccountRequest
{
    public string UserName { get; set; } = null!;

    [DataType(DataType.Password)]
    public string? Password { get; set; }

    public string? Name { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string EmailAddress { get; set; } = null!;
}