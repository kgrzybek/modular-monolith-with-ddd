using Autofac;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Services.IdentityTokenService;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration.Services;

internal class ServicesModule : Module
{
    private readonly IUserAccessConfiguration _userAccessConfiguration;

    public ServicesModule(IUserAccessConfiguration userAccessConfiguration)
    {
        _userAccessConfiguration = userAccessConfiguration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<IdentityTokenClaimService>()
            .As<ITokenClaimsService>()
            .InstancePerLifetimeScope();

        builder.Register(x => _userAccessConfiguration)
            .As<IUserAccessConfiguration>();
    }
}