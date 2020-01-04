using System.Reflection;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;

namespace CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(InternalCommandBase).Assembly;
    }
}