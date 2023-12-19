using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration.Identity;

public class EmailConfirmationTokenProvider<TUser> : DataProtectorTokenProvider<TUser>
    where TUser : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailConfirmationTokenProvider{TUser}"/> class.
    /// </summary>
    /// <param name="dataProtectionProvider">The system data protection provider.</param>
    /// <param name="options">The configured <see cref="Microsoft.AspNetCore.Identity.DataProtectionTokenProviderOptions"/>.</param>
    /// <param name="logger">The logger.</param>
    public EmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<EmailConfirmationTokenProviderOptions> options, ILogger<EmailConfirmationTokenProvider<TUser>> logger)
        : base(dataProtectionProvider, options, logger)
    {
    }
}