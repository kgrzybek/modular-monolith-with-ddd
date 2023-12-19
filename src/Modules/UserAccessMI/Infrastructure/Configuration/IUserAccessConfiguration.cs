using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration;

public interface IUserAccessConfiguration
{
    public Security? Security { get; set; }
}

public static class UserAccessConfigurationExtensions
{
    public static string? GetValidAudience(this IUserAccessConfiguration configuration)
        => configuration.Security?.JwtAudience;

    public static bool ShouldValidateAudience(this IUserAccessConfiguration configuration)
        => !string.IsNullOrEmpty(configuration.GetValidAudience());

    public static string? GetValidIssuer(this IUserAccessConfiguration configuration)
        => configuration.Security?.JwtIssuer;

    public static bool ShouldValidateIssuer(this IUserAccessConfiguration configuration)
        => !string.IsNullOrEmpty(configuration.GetValidIssuer());

    public static byte[] GetJwtSecretKeyEncrypted(this IUserAccessConfiguration configuration)
        => Encoding.ASCII.GetBytes(configuration.Security?.JwtSecretKey ?? string.Empty);

    public static SymmetricSecurityKey GetIssuerSigningKey(this IUserAccessConfiguration configuration)
        => new SymmetricSecurityKey(configuration.GetJwtSecretKeyEncrypted());
}