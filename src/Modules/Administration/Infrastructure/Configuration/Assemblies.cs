using System.Reflection;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(InternalCommandBase).Assembly;
    }
}