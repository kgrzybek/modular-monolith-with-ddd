using Autofac;
using Autofac.Extensions.DependencyInjection;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration.Identity;

internal class IdentityModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        serviceCollection.AddIdentity<ApplicationUser, Role>(options =>
        {
            options.SignIn.RequireConfirmedEmail = false;

            // Configure password policy
            options.Password.RequiredUniqueChars = 1;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 1;
            options.Password.RequireNonAlphanumeric = false;

            // Configure user policy
            options.User.RequireUniqueEmail = false;

            // Protecting against brute-force attacks with user lockout
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        })
        .AddEntityFrameworkStores<UserAccessContext>()
        .AddDefaultTokenProviders()
        .AddPasswordValidator<DoesNotContainPasswordValidator<ApplicationUser>>();

        serviceCollection.AddOptions();
        serviceCollection.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromHours(3));
        serviceCollection.Configure<EmailConfirmationTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromDays(2));

        builder.Populate(serviceCollection);
    }
}