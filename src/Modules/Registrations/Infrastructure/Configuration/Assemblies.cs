using System.Reflection;
using CompanyName.MyMeetings.Modules.Registrations.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Registrations.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(IRegistrationsModule).Assembly;
    }
}