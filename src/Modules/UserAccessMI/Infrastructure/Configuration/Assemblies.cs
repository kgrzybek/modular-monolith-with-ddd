using System.Reflection;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration;

internal static class Assemblies
{
    public static readonly Assembly Application = typeof(IUserAccessModule).Assembly;
}