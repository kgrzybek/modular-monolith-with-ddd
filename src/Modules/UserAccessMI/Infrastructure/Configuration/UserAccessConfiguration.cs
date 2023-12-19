namespace CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration;

public class UserAccessConfiguration : IUserAccessConfiguration
{
    public Security? Security { get; set; }
}

public class Security
{
    public string? JwtSecretKey { get; set; }

    public string? JwtIssuer { get; set; }

    public string? JwtAudience { get; set; }
}