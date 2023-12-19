using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration;

namespace CompanyName.MyMeetings.API.Configuration.Extensions
{
    internal static class ConfigurationExtensions
    {
        public static IUserAccessConfiguration GetUserAccessConfiguration(this IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

            var userManagementSection = configuration.GetSection("Modules:UserAccess");
            return userManagementSection.Get<UserAccessConfiguration>();
        }
    }
}